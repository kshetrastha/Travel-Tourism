using TravelAndTours.Web.Models;

namespace TravelAndTours.Web.Services;

public sealed class ProductsService : IProductsService
{
    private readonly ApiClient _api;

    public ProductsService(ApiClient api)
    {
        _api = api;
    }

    public async Task<List<ProductDto>> GetAllAsync(CancellationToken ct = default)
    {
        // Expected API endpoint: GET /api/products
        return await _api.GetAsync<List<ProductDto>>("api/products", ct) ?? new List<ProductDto>();
    }

    public Task<ProductDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        // Expected API endpoint: GET /api/products/{id}
        return _api.GetAsync<ProductDto>($"api/products/{id}", ct);
    }

    public async Task CreateAsync(ProductUpsertViewModel vm, CancellationToken ct = default)
    {
        // Expected API endpoint: POST /api/products
        await _api.PostAsync<ProductUpsertViewModel, object>("api/products", vm, ct);
    }

    public Task UpdateAsync(ProductUpsertViewModel vm, CancellationToken ct = default)
    {
        // Expected API endpoint: PUT /api/products/{id}
        return _api.PutAsync($"api/products/{vm.Id}", vm, ct);
    }

    public Task DeleteAsync(int id, CancellationToken ct = default)
    {
        // Expected API endpoint: DELETE /api/products/{id}
        return _api.DeleteAsync($"api/products/{id}", ct);
    }
}
