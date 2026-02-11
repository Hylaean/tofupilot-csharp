# Porting Surprises

Things discovered while building the C# SDK against the v2 API that could be simplified or made more consistent.

---

## 1. Inconsistent resource identifiers in URL paths

Some resources use UUIDs, others use human-readable identifiers:

| Resource | Identifier | Example |
|---|---|---|
| Procedures | UUID | `GET /v2/procedures/{uuid}` |
| Stations | UUID | `GET /v2/stations/{uuid}` |
| Runs | UUID | `GET /v2/runs/{uuid}` |
| Parts | part number | `GET /v2/parts/{number}` |
| Batches | batch number | `GET /v2/batches/{number}` |
| Units | serial number | `GET /v2/units/{serial_number}` |
| Part Revisions | part number + revision number | `GET /v2/parts/{pn}/revisions/{rn}` |
| Procedure Versions | UUID + tag string | `GET /v2/procedures/{uuid}/versions/{tag}` |

This forces SDK authors to track which identifier type each resource uses. A uniform approach (all UUIDs, or all human-readable) would simplify client code significantly.

---

## 2. Inconsistent rename field names across update endpoints

When renaming the primary identifier of a resource, the field name varies:

| Resource | Update field | Value |
|---|---|---|
| Part | `new_number` | new part number |
| Batch | `new_number` | new batch number |
| Part Revision | `number` | new revision number |
| Unit | `new_serial_number` | new serial number |

Part revisions use `number` while parts and batches use `new_number`. Having a single convention (always `new_number` or always the original field name) would remove a source of bugs.

---

## 3. Datetime format: `Z` required, `+00:00` rejected

The API rejects ISO 8601 datetimes with `+00:00` offset and only accepts the `Z` suffix:

```
"2025-01-01T00:00:00Z"         -> accepted
"2025-01-01T00:00:00+00:00"    -> "Invalid datetime"
```

Both are valid ISO 8601 representations of the same instant. Many languages (including .NET's `DateTimeOffset`) default to `+00:00`. We had to write a custom serializer just for this. Accepting both formats would save every SDK from needing a workaround.

---

## 4. Procedure PATCH requires `name` even when not changing it

```json
PATCH /v2/procedures/{id}
{"description": "Updated description"}
-> 400: "name: Required"
```

For a PATCH endpoint, all fields should be optional. Having to re-send `name` to update only `description` defeats the purpose of PATCH vs PUT.

---

## 5. Run logs require `source_file` and `line_number`

Creating a run with logs fails if `source_file` or `line_number` are omitted:

```
"logs.0.source_file": Required
"logs.0.line_number": Expected number, received nan
```

These feel like optional metadata. Not every test framework or language tracks source file locations. Making them optional would lower the barrier for log inclusion.

---

## 6. Run phases use different time fields than expected

Phase timing uses `started_at` / `ended_at` (datetime strings), while the field names `start_time_millis` / `end_time_millis` (used in the Python SDK's internal representation) suggest epoch milliseconds. The actual API expects datetime strings matching the run-level `started_at` / `ended_at` format, which is consistent but wasn't obvious from the field naming in existing SDK code.

---

## 7. Create responses return only `{"id": "..."}`

All create endpoints return a minimal response:

```json
POST /v2/parts -> {"id": "uuid"}
POST /v2/units -> {"id": "uuid"}
```

But for parts, batches, and units, subsequent operations require the human-readable identifier (part number, serial number), not the UUID. This forces an extra GET call after every create if you need to chain operations. Returning at least the primary identifier in the create response would eliminate these round-trips:

```json
POST /v2/parts -> {"id": "uuid", "number": "PART-001"}
POST /v2/units -> {"id": "uuid", "serial_number": "SN-001"}
```

---

## 8. GET run response uses nested objects, unlike create request

The create request uses flat fields:

```json
POST: {"serial_number": "SN1", "part_number": "P1", "procedure_id": "uuid"}
```

But the GET response nests them:

```json
GET: {"unit": {"serial_number": "SN1", "part": {"number": "P1"}}, "procedure": {"id": "uuid"}}
```

This asymmetry means SDK response models can't mirror request models. Either flattening the GET response or nesting the POST request would simplify SDK design.

---

## 9. Unit delete uses query params instead of path

All other single-resource deletes use the path:

```
DELETE /v2/parts/{number}
DELETE /v2/batches/{number}
DELETE /v2/stations/{id}
```

But units use a query parameter:

```
DELETE /v2/units?serial_numbers=SN001
```

This is useful for bulk delete but breaks the pattern. Supporting both `DELETE /v2/units/{serial_number}` (single) and query params (bulk) would be more consistent.

---

## 10. Attachment delete endpoint returns 404

`DELETE /v2/attachments/{id}` returns 404 regardless of the attachment ID. The endpoint appears to not be implemented yet but is documented.

---

## 11. Station link/unlink procedure endpoints don't exist

The v2 API has no endpoints for linking or unlinking procedures to stations. If this is planned, it would be good to know; if not, it should be removed from any API documentation.

---

## 12. Validators in measurement response are objects, not simple values

When creating a measurement, you pass `lower_limit` and `upper_limit`. But the GET response returns a `validators` array of rich objects:

```json
"validators": [
  {"outcome": "PASS", "operator": ">=", "expected_value": 4, "expression": "x >= 4", ...},
  {"outcome": "PASS", "operator": "<=", "expected_value": 6, "expression": "x <= 6", ...}
]
```

This is fine for display but surprising when you expected to get back the simple limits you sent. Documenting this transformation would help SDK authors model their response types correctly.

---

*Compiled during C# SDK development, February 2026.*
