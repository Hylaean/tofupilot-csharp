# API Coverage

Tracks alignment between the [TofuPilot OpenAPI v2 spec](https://www.tofupilot.app/api/v2/openapi.json) and this SDK.

- **Spec version**: 2.0.0
- **Last checked**: 2026-03-31

## Coverage Table

| operationId | Method | Path | Summary | SDK Method | Status |
|---|---|---|---|---|---|
| **Runs** | | | | | |
| `run-list` | GET | `/v2/runs` | List and filter runs | `Runs.ListAsync` | Implemented |
| `run-create` | POST | `/v2/runs` | Create run | `Runs.CreateAsync` | Implemented |
| `run-get` | GET | `/v2/runs/{id}` | Get run | `Runs.GetAsync` | Implemented |
| `run-update` | PATCH | `/v2/runs/{id}` | Update run | `Runs.UpdateAsync` | Implemented |
| `run-delete` | DELETE | `/v2/runs` | Delete runs | `Runs.DeleteAsync` | Implemented |
| **Units** | | | | | |
| `unit-list` | GET | `/v2/units` | List and filter units | `Units.ListAsync` | Implemented |
| `unit-create` | POST | `/v2/units` | Create unit | `Units.CreateAsync` | Implemented |
| `unit-get` | GET | `/v2/units/{serial_number}` | Get unit | `Units.GetAsync` | Implemented |
| `unit-update` | PATCH | `/v2/units/{serial_number}` | Update unit | `Units.UpdateAsync` | Implemented |
| `unit-delete` | DELETE | `/v2/units` | Delete units | `Units.DeleteAsync` | Implemented |
| `unit-addChild` | PUT | `/v2/units/{serial_number}/children` | Add sub-unit | `Units.AddChildAsync` | Implemented |
| `unit-removeChild` | DELETE | `/v2/units/{serial_number}/children` | Remove sub-unit | `Units.RemoveChildAsync` | Implemented |
| **Procedures** | | | | | |
| `procedure-list` | GET | `/v2/procedures` | List and filter procedures | `Procedures.ListAsync` | Implemented |
| `procedure-create` | POST | `/v2/procedures` | Create procedure | `Procedures.CreateAsync` | Implemented |
| `procedure-get` | GET | `/v2/procedures/{id}` | Get procedure | `Procedures.GetAsync` | Implemented |
| `procedure-update` | PATCH | `/v2/procedures/{id}` | Update procedure | `Procedures.UpdateAsync` | Implemented |
| `procedure-delete` | DELETE | `/v2/procedures/{id}` | Delete procedure | `Procedures.DeleteAsync` | Implemented |
| **Procedure Versions** | | | | | |
| `procedure-createVersion` | POST | `/v2/procedures/{id}/versions` | Create procedure version | `Procedures.Versions.CreateAsync` | Implemented |
| `procedure-getVersion` | GET | `/v2/procedures/{id}/versions/{tag}` | Get procedure version | `Procedures.Versions.GetAsync` | Implemented |
| `procedure-deleteVersion` | DELETE | `/v2/procedures/{id}/versions/{tag}` | Delete procedure version | `Procedures.Versions.DeleteAsync` | Implemented |
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
| **Attachments** | | | | | |
| `attachment-initialize` | POST | `/v2/attachments` | Initialize upload | `Attachments.InitializeAsync` | Implemented |
| `attachment-finalize` | POST | `/v2/attachments/{id}/finalize` | Finalize upload | `Attachments.FinalizeAsync` | Implemented |
| `attachment-delete` | DELETE | `/v2/attachments` | Delete attachments | `Attachments.DeleteAsync` | Implemented |
| **Users** | | | | | |
| `user-list` | GET | `/v2/users` | List users | `Users.ListAsync` | Implemented |

## Summary

| | Count |
|---|---|
| OpenAPI operations | 45 |
| Implemented | 45 |
| Missing from SDK | 0 |
| SDK extras (no API backing) | 0 |
