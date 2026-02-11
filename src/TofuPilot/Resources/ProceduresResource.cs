using TofuPilot.Models.Common;
using TofuPilot.Models.Procedures;

namespace TofuPilot.Resources;

/// <summary>
/// Resource for managing procedures.
/// </summary>
public sealed class ProceduresResource(ITofuPilotHttpClient httpClient) : ResourceBase(httpClient)
{
    /// <inheritdoc/>
    protected override string BasePath => "v2/procedures";

    /// <summary>Gets the versions sub-resource.</summary>
    public ProcedureVersionsResource Versions { get; } = new(httpClient);

    /// <summary>Lists procedures with optional filtering.</summary>
    public async Task<PaginatedResponse<Procedure>> ListAsync(
        ListProceduresRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new ListProceduresRequest();

        var queryParams = new Dictionary<string, object?>
        {
            ["search_query"] = request.SearchQuery,
            ["ids"] = request.Ids,
            ["limit"] = request.Limit?.ToString(),
            ["cursor"] = request.Cursor?.ToString(),
        };

        var uri = BuildUriWithArrayParams(BasePath, queryParams);
        return await HttpClient.GetAsync<PaginatedResponse<Procedure>>(uri, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Creates a new procedure.</summary>
    public Task<Procedure> CreateAsync(CreateProcedureRequest request, CancellationToken cancellationToken = default) =>
        HttpClient.PostAsync<CreateProcedureRequest, Procedure>(BasePath, request, cancellationToken);

    /// <summary>Gets a procedure by ID.</summary>
    public Task<Procedure> GetAsync(string id, CancellationToken cancellationToken = default) =>
        HttpClient.GetAsync<Procedure>($"{BasePath}/{id}", cancellationToken);

    /// <summary>Updates a procedure.</summary>
    public Task<Procedure> UpdateAsync(string id, UpdateProcedureRequest request, CancellationToken cancellationToken = default) =>
        HttpClient.PatchAsync<UpdateProcedureRequest, Procedure>($"{BasePath}/{id}", request, cancellationToken);

    /// <summary>Deletes a procedure by ID.</summary>
    public Task<DeleteResponse> DeleteAsync(string id, CancellationToken cancellationToken = default) =>
        HttpClient.DeleteAsync<DeleteResponse>($"{BasePath}/{id}", cancellationToken);
}

/// <summary>
/// Resource for managing procedure versions.
/// </summary>
public sealed class ProcedureVersionsResource(ITofuPilotHttpClient httpClient) : ResourceBase(httpClient)
{
    /// <inheritdoc/>
    protected override string BasePath => "v2/procedures";

    /// <summary>Lists versions for a procedure.</summary>
    public Task<PaginatedResponse<ProcedureVersion>> ListAsync(string procedureId, CancellationToken cancellationToken = default) =>
        HttpClient.GetAsync<PaginatedResponse<ProcedureVersion>>($"{BasePath}/{procedureId}/versions", cancellationToken);

    /// <summary>Creates a new version for a procedure.</summary>
    public Task<ProcedureVersion> CreateAsync(string procedureId, CreateProcedureVersionRequest request, CancellationToken cancellationToken = default) =>
        HttpClient.PostAsync<CreateProcedureVersionRequest, ProcedureVersion>($"{BasePath}/{procedureId}/versions", request, cancellationToken);

    /// <summary>Gets a procedure version by tag.</summary>
    public Task<ProcedureVersion> GetAsync(string procedureId, string tag, CancellationToken cancellationToken = default) =>
        HttpClient.GetAsync<ProcedureVersion>($"{BasePath}/{procedureId}/versions/{tag}", cancellationToken);

    /// <summary>Deletes a procedure version by tag.</summary>
    public Task<DeleteResponse> DeleteAsync(string procedureId, string tag, CancellationToken cancellationToken = default) =>
        HttpClient.DeleteAsync<DeleteResponse>($"{BasePath}/{procedureId}/versions/{tag}", cancellationToken);
}
