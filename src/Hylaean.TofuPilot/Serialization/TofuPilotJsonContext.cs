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
[JsonSerializable(typeof(Run))]
[JsonSerializable(typeof(CreateRunRequest))]
[JsonSerializable(typeof(UpdateRunRequest))]
[JsonSerializable(typeof(PaginatedResponse<Run>))]
[JsonSerializable(typeof(Unit))]
[JsonSerializable(typeof(CreateUnitRequest))]
[JsonSerializable(typeof(UpdateUnitRequest))]
[JsonSerializable(typeof(PaginatedResponse<Unit>))]
[JsonSerializable(typeof(Procedure))]
[JsonSerializable(typeof(ProcedureVersion))]
[JsonSerializable(typeof(CreateProcedureRequest))]
[JsonSerializable(typeof(UpdateProcedureRequest))]
[JsonSerializable(typeof(CreateProcedureVersionRequest))]
[JsonSerializable(typeof(PaginatedResponse<Procedure>))]
[JsonSerializable(typeof(PaginatedResponse<ProcedureVersion>))]
[JsonSerializable(typeof(Part))]
[JsonSerializable(typeof(PartRevision))]
[JsonSerializable(typeof(CreatePartRequest))]
[JsonSerializable(typeof(UpdatePartRequest))]
[JsonSerializable(typeof(CreatePartRevisionRequest))]
[JsonSerializable(typeof(UpdatePartRevisionRequest))]
[JsonSerializable(typeof(PaginatedResponse<Part>))]
[JsonSerializable(typeof(PaginatedResponse<PartRevision>))]
[JsonSerializable(typeof(Batch))]
[JsonSerializable(typeof(CreateBatchRequest))]
[JsonSerializable(typeof(UpdateBatchRequest))]
[JsonSerializable(typeof(PaginatedResponse<Batch>))]
[JsonSerializable(typeof(RunUnit))]
[JsonSerializable(typeof(RunUnitPart))]
[JsonSerializable(typeof(RunUnitPartRevision))]
[JsonSerializable(typeof(RunUnitBatch))]
[JsonSerializable(typeof(RunProcedure))]
[JsonSerializable(typeof(RunProcedureVersion))]
[JsonSerializable(typeof(UnitPart))]
[JsonSerializable(typeof(UnitPartRevision))]
[JsonSerializable(typeof(UnitBatch))]
[JsonSerializable(typeof(MeasurementValidator))]
[JsonSerializable(typeof(Station))]
[JsonSerializable(typeof(CreateStationRequest))]
[JsonSerializable(typeof(UpdateStationRequest))]
[JsonSerializable(typeof(PaginatedResponse<Station>))]
[JsonSerializable(typeof(InitializeUploadRequest))]
[JsonSerializable(typeof(InitializeUploadResponse))]
[JsonSerializable(typeof(FinalizeUploadResponse))]
[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(List<User>))]
[JsonSerializable(typeof(AddChildRequest))]
[JsonSerializable(typeof(DeleteResponse))]
[JsonSerializable(typeof(BulkDeleteResponse))]
[JsonSerializable(typeof(DeletePartResponse))]
[JsonSerializable(typeof(PaginationMeta))]
public partial class TofuPilotJsonContext : JsonSerializerContext
{
}
