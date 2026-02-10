using System.Text.Json;
using System.Text.Json.Serialization;
using TofuPilot.Models.Attachments;
using TofuPilot.Models.Batches;
using TofuPilot.Models.Common;
using TofuPilot.Models.Parts;
using TofuPilot.Models.Procedures;
using TofuPilot.Models.Runs;
using TofuPilot.Models.Stations;
using TofuPilot.Models.Units;

namespace TofuPilot.Serialization;

/// <summary>
/// JSON serialization context for TofuPilot SDK.
/// </summary>
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
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
[JsonSerializable(typeof(Station))]
[JsonSerializable(typeof(CreateStationRequest))]
[JsonSerializable(typeof(UpdateStationRequest))]
[JsonSerializable(typeof(LinkProcedureRequest))]
[JsonSerializable(typeof(PaginatedResponse<Station>))]
[JsonSerializable(typeof(InitializeUploadRequest))]
[JsonSerializable(typeof(InitializeUploadResponse))]
[JsonSerializable(typeof(DeleteAttachmentResponse))]
[JsonSerializable(typeof(DeleteResponse))]
public partial class TofuPilotJsonContext : JsonSerializerContext
{
}
