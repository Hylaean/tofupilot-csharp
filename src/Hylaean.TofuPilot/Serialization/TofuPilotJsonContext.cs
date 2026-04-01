using System.Text.Json;
using System.Text.Json.Serialization;
using Hylaean.TofuPilot.Models.Attachments;
using Hylaean.TofuPilot.Models.Batches;
using Hylaean.TofuPilot.Models.Common;
using Hylaean.TofuPilot.Models.Parts;
using Hylaean.TofuPilot.Models.Procedures;
using Hylaean.TofuPilot.Models.Runs;
using Hylaean.TofuPilot.Models.Stations;
using Hylaean.TofuPilot.Models.Units;
using Hylaean.TofuPilot.Models.Users;

namespace Hylaean.TofuPilot.Serialization;

/// <summary>
/// JSON serialization context for TofuPilot SDK.
/// </summary>
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    WriteIndented = false)]
// Common
[JsonSerializable(typeof(CreatedByUser))]
[JsonSerializable(typeof(CreatedByStation))]
[JsonSerializable(typeof(DeleteResponse))]
[JsonSerializable(typeof(BulkDeleteResponse))]
[JsonSerializable(typeof(DeletePartResponse))]
[JsonSerializable(typeof(PaginationMeta))]
// Runs
[JsonSerializable(typeof(Run))]
[JsonSerializable(typeof(CreateRunRequest))]
[JsonSerializable(typeof(UpdateRunRequest))]
[JsonSerializable(typeof(PaginatedResponse<Run>))]
[JsonSerializable(typeof(RunUnit))]
[JsonSerializable(typeof(RunUnitPart))]
[JsonSerializable(typeof(RunUnitPartRevision))]
[JsonSerializable(typeof(RunUnitBatch))]
[JsonSerializable(typeof(RunProcedure))]
[JsonSerializable(typeof(RunProcedureVersion))]
[JsonSerializable(typeof(RunSubUnit))]
[JsonSerializable(typeof(MeasurementValidator))]
[JsonSerializable(typeof(MeasurementDataSeries))]
// Units
[JsonSerializable(typeof(Unit))]
[JsonSerializable(typeof(CreateUnitRequest))]
[JsonSerializable(typeof(UpdateUnitRequest))]
[JsonSerializable(typeof(AddChildRequest))]
[JsonSerializable(typeof(PaginatedResponse<Unit>))]
[JsonSerializable(typeof(UnitPart))]
[JsonSerializable(typeof(UnitPartRevision))]
[JsonSerializable(typeof(UnitBatch))]
[JsonSerializable(typeof(UnitCreatedDuring))]
[JsonSerializable(typeof(UnitCreatedDuringProcedure))]
[JsonSerializable(typeof(UnitAttachment))]
[JsonSerializable(typeof(UnitLastRun))]
[JsonSerializable(typeof(UnitLastRunProcedure))]
// Procedures
[JsonSerializable(typeof(Procedure))]
[JsonSerializable(typeof(ProcedureVersion))]
[JsonSerializable(typeof(CreateProcedureRequest))]
[JsonSerializable(typeof(UpdateProcedureRequest))]
[JsonSerializable(typeof(CreateProcedureVersionRequest))]
[JsonSerializable(typeof(PaginatedResponse<Procedure>))]
[JsonSerializable(typeof(PaginatedResponse<ProcedureVersion>))]
[JsonSerializable(typeof(ProcedureRun))]
[JsonSerializable(typeof(ProcedureRunUnit))]
[JsonSerializable(typeof(ProcedureStation))]
[JsonSerializable(typeof(ProcedureLinkedRepository))]
[JsonSerializable(typeof(ProcedureVersionProcedure))]
// Parts
[JsonSerializable(typeof(Part))]
[JsonSerializable(typeof(PartRevision))]
[JsonSerializable(typeof(CreatePartRequest))]
[JsonSerializable(typeof(UpdatePartRequest))]
[JsonSerializable(typeof(CreatePartRevisionRequest))]
[JsonSerializable(typeof(UpdatePartRevisionRequest))]
[JsonSerializable(typeof(PaginatedResponse<Part>))]
[JsonSerializable(typeof(PaginatedResponse<PartRevision>))]
[JsonSerializable(typeof(PartRevisionPart))]
[JsonSerializable(typeof(PartRevisionUnit))]
// Batches
[JsonSerializable(typeof(Batch))]
[JsonSerializable(typeof(CreateBatchRequest))]
[JsonSerializable(typeof(UpdateBatchRequest))]
[JsonSerializable(typeof(PaginatedResponse<Batch>))]
[JsonSerializable(typeof(BatchUnit))]
[JsonSerializable(typeof(BatchUnitPart))]
[JsonSerializable(typeof(BatchUnitPartRevision))]
// Stations
[JsonSerializable(typeof(Station))]
[JsonSerializable(typeof(CreateStationRequest))]
[JsonSerializable(typeof(UpdateStationRequest))]
[JsonSerializable(typeof(PaginatedResponse<Station>))]
[JsonSerializable(typeof(StationProcedure))]
[JsonSerializable(typeof(StationProcedureDeployment))]
[JsonSerializable(typeof(StationDeploymentCommit))]
[JsonSerializable(typeof(StationDeploymentRepository))]
[JsonSerializable(typeof(StationTeam))]
// Attachments
[JsonSerializable(typeof(InitializeUploadRequest))]
[JsonSerializable(typeof(InitializeUploadResponse))]
[JsonSerializable(typeof(FinalizeUploadResponse))]
// Users
[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(List<User>))]
public partial class TofuPilotJsonContext : JsonSerializerContext
{
}
