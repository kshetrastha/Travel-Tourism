using TravelAndTours.Web.Models;

namespace TravelAndTours.Web.Services;

public interface IExpeditionsService
{
    Task<PagedResult<ExpeditionCardDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);
}
