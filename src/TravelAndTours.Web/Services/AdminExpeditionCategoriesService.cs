using TravelAndTours.Web.Models;

namespace TravelAndTours.Web.Services;

public sealed class AdminExpeditionCategoriesService : IAdminExpeditionCategoriesService
{
    private readonly ApiClient _api;

    public AdminExpeditionCategoriesService(ApiClient api)
    {
        _api = api;
    }

    public async Task<List<ExpeditionCategoryDto>> GetAllAsync(CancellationToken ct = default)
        => await _api.GetAsync<List<ExpeditionCategoryDto>>("api/admin/expedition-categories", ct) ?? [];

    public Task<ExpeditionCategoryDto?> GetByIdAsync(int id, CancellationToken ct = default)
        => _api.GetAsync<ExpeditionCategoryDto>($"api/admin/expedition-categories/{id}", ct);

    public Task<ApiResponse<int>?> CreateAsync(ExpeditionCategoryUpsertViewModel vm, CancellationToken ct = default)
        => _api.PostAsync<ExpeditionCategoryUpsertRequest, ApiResponse<int>>(
            "api/admin/expedition-categories",
            ExpeditionCategoryUpsertRequest.From(vm),
            ct);

    public Task<ApiResponse<bool>?> UpdateAsync(int id, ExpeditionCategoryUpsertViewModel vm, CancellationToken ct = default)
        => _api.PutAsync<ExpeditionCategoryUpsertRequest, ApiResponse<bool>>(
            $"api/admin/expedition-categories/{id}",
            ExpeditionCategoryUpsertRequest.From(vm),
            ct);

    public Task<ApiResponse<bool>?> DeleteAsync(int id, CancellationToken ct = default)
        => _api.DeleteAsync<ApiResponse<bool>>($"api/admin/expedition-categories/{id}", ct);

    private sealed record ExpeditionCategoryUpsertRequest(
        string Name,
        string Slug,
        int? ParentCategoryId,
        int SortOrder,
        bool IsActive)
    {
        public static ExpeditionCategoryUpsertRequest From(ExpeditionCategoryUpsertViewModel vm)
            => new(
                vm.Name,
                vm.Slug,
                vm.ParentCategoryId,
                vm.SortOrder,
                vm.IsActive);
    }
}
