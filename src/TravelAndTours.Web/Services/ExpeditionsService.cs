using TravelAndTours.Web.Models;

namespace TravelAndTours.Web.Services;

public sealed class ExpeditionsService : IExpeditionsService
{
    private readonly ApiClient _api;

    public ExpeditionsService(ApiClient api)
    {
        _api = api;
    }

    public async Task<PagedResult<ExpeditionCardDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
        => await _api.GetAsync<PagedResult<ExpeditionCardDto>>($"api/expeditions?page={page}&pageSize={pageSize}", ct)
           ?? new PagedResult<ExpeditionCardDto>([], page, pageSize, 0, 0, false, false);
}
