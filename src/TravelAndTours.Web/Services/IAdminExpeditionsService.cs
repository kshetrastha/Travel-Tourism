using TravelAndTours.Web.Models;

namespace TravelAndTours.Web.Services;

public interface IAdminExpeditionsService
{
    Task<PagedResult<AdminExpeditionListItemDto>> GetAllAsync(int page, int pageSize, CancellationToken ct = default);
    Task<AdminExpeditionDetailDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<List<ExpeditionCategoryDto>> GetCategoriesAsync(CancellationToken ct = default);
    Task<ApiResponse<int>?> CreateAsync(ExpeditionUpsertViewModel vm, CancellationToken ct = default);
    Task<ApiResponse<bool>?> UpdateAsync(int id, ExpeditionUpsertViewModel vm, CancellationToken ct = default);
    Task<ApiResponse<bool>?> DeleteAsync(int id, CancellationToken ct = default);
    Task<ApiResponse<bool>?> PublishAsync(int id, CancellationToken ct = default);
    Task<ApiResponse<bool>?> UnpublishAsync(int id, CancellationToken ct = default);
    Task<ApiResponse<bool>?> ReplaceItineraryAsync(int id, IReadOnlyList<ItineraryDayInputViewModel> days, CancellationToken ct = default);
    Task<ApiResponse<bool>?> ReplaceFixedDeparturesAsync(int id, IReadOnlyList<FixedDepartureInputViewModel> departures, CancellationToken ct = default);
    Task<ApiResponse<bool>?> ReplaceMediaAsync(int id, IReadOnlyList<MediaAssetInputViewModel> media, CancellationToken ct = default);
}
