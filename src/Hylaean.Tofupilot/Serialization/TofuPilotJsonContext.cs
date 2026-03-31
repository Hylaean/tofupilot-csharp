using System.Text.Json;
using System.Text.Json.Serialization;
using Hylaean.Tofupilot.Models.Attachments;
using Hylaean.Tofupilot.Models.Batches;
using Hylaean.Tofupilot.Models.Common;
using Hylaean.Tofupilot.Models.Parts;
using Hylaean.Tofupilot.Models.Procedures;
using Hylaean.Tofupilot.Models.Runs;
using Hylaean.Tofupilot.Models.Stations;
using Hylaean.Tofupilot.Models.Units;

namespace Hylaean.Tofupilot.Serialization;

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
[JsonSerializable(typeof(MeasurementValidator))]
[JsonSerializable(typeof(Station))]
[JsonSerializable(typeof(CreateStationRequest))]
[JsonSerializable(typeof(UpdateStationRequest))]
[JsonSerializable(typeof(PaginatedResponse<Station>))]
[JsonSerializable(typeof(InitializeUploadRequest))]
[JsonSerializable(typeof(InitializeUploadResponse))]
[JsonSerializable(typeof(DeleteAttachmentResponse))]
[JsonSerializable(typeof(AddChildRequest))]
[JsonSerializable(typeof(DeleteResponse))]
[JsonSerializable(typeof(PaginationMeta))]
public partial class TofuPilotJsonContext : JsonSerializerContext
{
}
