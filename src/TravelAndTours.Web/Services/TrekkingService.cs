using TravelAndTours.Web.Models;

namespace TravelAndTours.Web.Services;

public sealed class TrekkingService : ITrekkingService
{
    private readonly ApiClient _api;

    public TrekkingService(ApiClient api)
    {
        _api = api;
    }

    public async Task<PagedResult<ExpeditionCardDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
        => await _api.GetAsync<PagedResult<ExpeditionCardDto>>($"api/trekking?page={page}&pageSize={pageSize}", ct)
           ?? new PagedResult<ExpeditionCardDto>([], page, pageSize, 0, 0, false, false);
}
