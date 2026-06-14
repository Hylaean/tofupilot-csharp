# API Coverage

Tracks alignment between the [TofuPilot OpenAPI v2 spec](https://www.tofupilot.app/api/v2/openapi.json) and this SDK,
benchmarked against the [official TofuPilot C# client](https://github.com/tofupilot/csharp), which implements the full spec surface.

- **Spec version**: 2.0.0
- **Spec operations**: 57
- **Last checked**: 2026-06-14

> **Status legend** — `Implemented`: exposed by this SDK. `Not implemented`: present in the spec / official client but not yet in this SDK.

## Coverage Table

| operationId | Method | Path | Summary | SDK Method | Status |
|---|---|---|---|---|---|
| **Runs** | | | | | |
| `run-list` | GET | `/v2/runs` | List and filter runs | `Runs.ListAsync` | Implemented |
| `run-create` | POST | `/v2/runs` | Create run | `Runs.CreateAsync` | Implemented |
| `run-get` | GET | `/v2/runs/{id}` | Get run | `Runs.GetAsync` | Implemented |
| `run-update` | PATCH | `/v2/runs/{id}` | Update run | `Runs.UpdateAsync` | Implemented |
| `run-delete` | DELETE | `/v2/runs` | Delete runs | `Runs.DeleteAsync` | Implemented |
| `run-createAttachment` | POST | `/v2/runs/{id}/attachments` | Attach file to run | — | Not implemented |
| `run-updateMetadata` | PATCH | `/v2/runs/{id}/metadata` | Update run metadata | — | Not implemented |
| **Units** | | | | | |
| `unit-list` | GET | `/v2/units` | List and filter units | `Units.ListAsync` | Implemented |
| `unit-create` | POST | `/v2/units` | Create unit | `Units.CreateAsync` | Implemented |
| `unit-get` | GET | `/v2/units/{serial_number}` | Get unit | `Units.GetAsync` | Implemented |
| `unit-update` | PATCH | `/v2/units/{serial_number}` | Update unit | `Units.UpdateAsync` | Implemented |
| `unit-delete` | DELETE | `/v2/units` | Delete units | `Units.DeleteAsync` | Implemented |
| `unit-addChild` | PUT | `/v2/units/{serial_number}/children` | Add sub-unit | `Units.AddChildAsync` | Implemented |
| `unit-removeChild` | DELETE | `/v2/units/{serial_number}/children` | Remove sub-unit | `Units.RemoveChildAsync` | Implemented |
| `unit-createAttachment` | POST | `/v2/units/{serial_number}/attachments` | Attach file to unit | — | Not implemented |
| `unit-deleteAttachment` | DELETE | `/v2/units/{serial_number}/attachments` | Delete unit attachments | — | Not implemented |
| **Procedures** | | | | | |
| `procedure-list` | GET | `/v2/procedures` | List and filter procedures | `Procedures.ListAsync` | Implemented |
| `procedure-create` | POST | `/v2/procedures` | Create procedure | `Procedures.CreateAsync` | Implemented |
| `procedure-get` | GET | `/v2/procedures/{id}` | Get procedure | `Procedures.GetAsync` | Implemented |
| `procedure-update` | PATCH | `/v2/procedures/{id}` | Update procedure | `Procedures.UpdateAsync` | Implemented |
| `procedure-delete` | DELETE | `/v2/procedures/{id}` | Delete procedure | `Procedures.DeleteAsync` | Implemented |
| **Procedure Versions** | | | | | |
| `procedure-createVersion` | POST | `/v2/procedures/{procedure_id}/versions` | Create procedure version | `Procedures.Versions.CreateAsync` | Implemented |
| `procedure-getVersion` | GET | `/v2/procedures/{procedure_id}/versions/{tag}` | Get procedure version | `Procedures.Versions.GetAsync` | Implemented |
| `procedure-deleteVersion` | DELETE | `/v2/procedures/{procedure_id}/versions/{tag}` | Delete procedure version | `Procedures.Versions.DeleteAsync` | Implemented |
| **Parts** | | | | | |
| `part-list` | GET | `/v2/parts` | List and filter parts | `Parts.ListAsync` | Implemented |
| `part-create` | POST | `/v2/parts` | Create part | `Parts.CreateAsync` | Implemented |
| `part-get` | GET | `/v2/parts/{number}` | Get part | `Parts.GetAsync` | Implemented |
| `part-update` | PATCH | `/v2/parts/{number}` | Update part | `Parts.UpdateAsync` | Implemented |
| `part-delete` | DELETE | `/v2/parts/{number}` | Delete part | `Parts.DeleteAsync` | Implemented |
| **Part Revisions** | | | | | |
| `part-createRevision` | POST | `/v2/parts/{part_number}/revisions` | Create part revision | `Parts.Revisions.CreateAsync` | Implemented |
| `part-getRevision` | GET | `/v2/parts/{part_number}/revisions/{revision_number}` | Get part revision | `Parts.Revisions.GetAsync` | Implemented |
| `part-updateRevision` | PATCH | `/v2/parts/{part_number}/revisions/{revision_number}` | Update part revision | `Parts.Revisions.UpdateAsync` | Implemented |
| `part-deleteRevision` | DELETE | `/v2/parts/{part_number}/revisions/{revision_number}` | Delete part revision | `Parts.Revisions.DeleteAsync` | Implemented |
| **Batches** | | | | | |
| `batch-list` | GET | `/v2/batches` | List and filter batches | `Batches.ListAsync` | Implemented |
| `batch-create` | POST | `/v2/batches` | Create batch | `Batches.CreateAsync` | Implemented |
| `batch-get` | GET | `/v2/batches/{number}` | Get batch | `Batches.GetAsync` | Implemented |
| `batch-update` | PATCH | `/v2/batches/{number}` | Update batch | `Batches.UpdateAsync` | Implemented |
| `batch-delete` | DELETE | `/v2/batches/{number}` | Delete batch | `Batches.DeleteAsync` | Implemented |
| **Stations** | | | | | |
| `station-list` | GET | `/v2/stations` | List and filter stations | `Stations.ListAsync` | Implemented |
| `station-create` | POST | `/v2/stations` | Create station | `Stations.CreateAsync` | Implemented |
| `station-getCurrent` | GET | `/v2/stations/current` | Get current station | `Stations.GetCurrentAsync` | Implemented |
| `station-get` | GET | `/v2/stations/{id}` | Get station | `Stations.GetAsync` | Implemented |
| `station-update` | PATCH | `/v2/stations/{id}` | Update station | `Stations.UpdateAsync` | Implemented |
| `station-remove` | DELETE | `/v2/stations/{id}` | Remove station | `Stations.RemoveAsync` | Implemented |
| **Deployments** | | | | | |
| `deployment-list` | GET | `/v2/deployments` | List and filter deployments | — | Not implemented |
| `deployment-get` | GET | `/v2/deployments/{id}` | Get deployment | — | Not implemented |
| **Logs** | | | | | |
| `log-list` | GET | `/v2/logs` | List and filter logs | — | Not implemented |
| `log-get` | GET | `/v2/logs/{id}` | Get log | — | Not implemented |
| **Measurements** | | | | | |
| `measurement-list` | GET | `/v2/measurements` | List and filter measurements | — | Not implemented |
| `measurement-get` | GET | `/v2/measurements/{id}` | Get measurement | — | Not implemented |
| **Phases** | | | | | |
| `phase-list` | GET | `/v2/phases` | List and filter phases | — | Not implemented |
| `phase-get` | GET | `/v2/phases/{id}` | Get phase | — | Not implemented |
| **Imports** | | | | | |
| `import-structured` | POST | `/v2/imports/structured` | Import runs from structured files | — | Not implemented |
| `import-tabular` | POST | `/v2/imports/tabular` | Import a run from a tabular file | — | Not implemented |
| **Attachments** | | | | | |
| `attachment-initialize` | POST | `/v2/attachments` | Initialize upload | `Attachments.InitializeAsync` | Implemented |
| `attachment-finalize` | POST | `/v2/attachments/{id}/finalize` | Finalize upload | `Attachments.FinalizeAsync` | Implemented |
| **Users** | | | | | |
| `user-list` | GET | `/v2/users` | List users | `Users.ListAsync` | Implemented |

## Not yet implemented

These operations exist in the spec and the official client but are not yet exposed by this SDK:

| Resource | Operations | Official client method |
|---|---|---|
| Runs | `run-createAttachment`, `run-updateMetadata` | `Runs.CreateAttachmentAsync`, `Runs.UpdateMetadataAsync` |
| Units | `unit-createAttachment`, `unit-deleteAttachment` | `Units.CreateAttachmentAsync`, `Units.DeleteAttachmentAsync` |
| Deployments | `deployment-list`, `deployment-get` | `Deployments.ListAsync`, `Deployments.GetAsync` |
| Logs | `log-list`, `log-get` | `Logs.ListAsync`, `Logs.GetAsync` |
| Measurements | `measurement-list`, `measurement-get` | `Measurements.ListAsync`, `Measurements.GetAsync` |
| Phases | `phase-list`, `phase-get` | `Phases.ListAsync`, `Phases.GetAsync` |
| Imports | `import-structured`, `import-tabular` | `Imports.StructuredAsync`, `Imports.TabularAsync` |

## Notes

- **Removed operation** — The former `attachment-delete` (`DELETE /v2/attachments`) is no longer in the spec; attachment deletion is now unit-scoped via `unit-deleteAttachment` (`DELETE /v2/units/{serial_number}/attachments`). This SDK's `Attachments.DeleteAsync` therefore no longer maps to a documented endpoint.
- **Convenience helpers** — The official client also ships file helpers that orchestrate several operations: `Runs.Attachments().UploadAsync`/`DownloadAsync` and `Units.Attachments().UploadAsync`/`DownloadAsync`/`DeleteAsync` (initialize → storage transfer → create/delete attachment). This SDK provides a comparable `Attachments.UploadAsync`. These wrap the operations above rather than mapping 1:1 to a single `operationId`.

## Summary

| | Count |
|---|---|
| OpenAPI operations | 57 |
| Implemented | 43 |
| Not implemented | 14 |
| SDK extras (no API backing) | 1 |
