using TravelAndTours.Web.Models;

namespace TravelAndTours.Web.Services;

public interface ITrekkingService
{
    Task<PagedResult<ExpeditionCardDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);
}
