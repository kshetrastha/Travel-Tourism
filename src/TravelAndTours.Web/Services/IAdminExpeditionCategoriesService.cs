using TravelAndTours.Web.Models;

namespace TravelAndTours.Web.Services;

public interface IAdminExpeditionCategoriesService
{
    Task<List<ExpeditionCategoryDto>> GetAllAsync(CancellationToken ct = default);
    Task<ExpeditionCategoryDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<ApiResponse<int>?> CreateAsync(ExpeditionCategoryUpsertViewModel vm, CancellationToken ct = default);
    Task<ApiResponse<bool>?> UpdateAsync(int id, ExpeditionCategoryUpsertViewModel vm, CancellationToken ct = default);
    Task<ApiResponse<bool>?> DeleteAsync(int id, CancellationToken ct = default);
}
