using TofuPilot.Http;
using TofuPilot.Models.Common;
using TofuPilot.Models.Procedures;

namespace TofuPilot.Resources;

/// <summary>
/// Resource for managing procedures.
/// </summary>
public sealed class ProceduresResource : ResourceBase
{
    /// <inheritdoc/>
    protected override string BasePath => "/v2/procedures";

    /// <summary>
    /// Gets the versions sub-resource.
    /// </summary>
    public ProcedureVersionsResource Versions { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProceduresResource"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client to use.</param>
    public ProceduresResource(ITofuPilotHttpClient httpClient) : base(httpClient)
    {
        Versions = new ProcedureVersionsResource(httpClient);
    }

    /// <summary>
    /// Lists procedures with optional filtering.
    /// </summary>
    /// <param name="request">The list request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of procedures.</returns>
    public async Task<PaginatedResponse<Procedure>> ListAsync(
        ListProceduresRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new ListProceduresRequest();

        var queryParams = new Dictionary<string, object?>
        {
            ["searchQuery"] = request.SearchQuery,
            ["ids"] = request.Ids,
            ["limit"] = request.Limit?.ToString(),
            ["cursor"] = request.Cursor?.ToString(),
        };

        var uri = BuildUriWithArrayParams(BasePath, queryParams);
        return await HttpClient.GetAsync<PaginatedResponse<Procedure>>(uri, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a new procedure.
    /// </summary>
    /// <param name="request">The create request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created procedure.</returns>
    public async Task<Procedure> CreateAsync(CreateProcedureRequest request, CancellationToken cancellationToken = default)
    {
        return await HttpClient.PostAsync<CreateProcedureRequest, Procedure>(BasePath, request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a procedure by ID.
    /// </summary>
    /// <param name="id">The procedure ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The procedure.</returns>
    public async Task<Procedure> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        return await HttpClient.GetAsync<Procedure>($"{BasePath}/{id}", cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Updates a procedure.
    /// </summary>
    /// <param name="id">The procedure ID.</param>
    /// <param name="request">The update request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated procedure.</returns>
    public async Task<Procedure> UpdateAsync(string id, UpdateProcedureRequest request, CancellationToken cancellationToken = default)
    {
        return await HttpClient.PatchAsync<UpdateProcedureRequest, Procedure>($"{BasePath}/{id}", request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a procedure by ID.
    /// </summary>
    /// <param name="id">The procedure ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The delete response.</returns>
    public async Task<DeleteResponse> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        return await HttpClient.DeleteAsync<DeleteResponse>($"{BasePath}/{id}", cancellationToken).ConfigureAwait(false);
    }
}

/// <summary>
/// Resource for managing procedure versions.
/// </summary>
public sealed class ProcedureVersionsResource : ResourceBase
{
    /// <inheritdoc/>
    protected override string BasePath => "/v2/procedures";

    /// <summary>
    /// Initializes a new instance of the <see cref="ProcedureVersionsResource"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client to use.</param>
    public ProcedureVersionsResource(ITofuPilotHttpClient httpClient) : base(httpClient)
    {
    }

    /// <summary>
    /// Lists versions for a procedure.
    /// </summary>
    /// <param name="procedureId">The procedure ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of procedure versions.</returns>
    public async Task<PaginatedResponse<ProcedureVersion>> ListAsync(string procedureId, CancellationToken cancellationToken = default)
    {
        return await HttpClient.GetAsync<PaginatedResponse<ProcedureVersion>>($"{BasePath}/{procedureId}/versions", cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a new version for a procedure.
    /// </summary>
    /// <param name="procedureId">The procedure ID.</param>
    /// <param name="request">The create request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created procedure version.</returns>
    public async Task<ProcedureVersion> CreateAsync(string procedureId, CreateProcedureVersionRequest request, CancellationToken cancellationToken = default)
    {
        return await HttpClient.PostAsync<CreateProcedureVersionRequest, ProcedureVersion>($"{BasePath}/{procedureId}/versions", request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a procedure version by ID.
    /// </summary>
    /// <param name="procedureId">The procedure ID.</param>
    /// <param name="versionId">The version ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The procedure version.</returns>
    public async Task<ProcedureVersion> GetAsync(string procedureId, string versionId, CancellationToken cancellationToken = default)
    {
        return await HttpClient.GetAsync<ProcedureVersion>($"{BasePath}/{procedureId}/versions/{versionId}", cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a procedure version.
    /// </summary>
    /// <param name="procedureId">The procedure ID.</param>
    /// <param name="versionId">The version ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The delete response.</returns>
    public async Task<DeleteResponse> DeleteAsync(string procedureId, string versionId, CancellationToken cancellationToken = default)
    {
        return await HttpClient.DeleteAsync<DeleteResponse>($"{BasePath}/{procedureId}/versions/{versionId}", cancellationToken).ConfigureAwait(false);
    }
}
