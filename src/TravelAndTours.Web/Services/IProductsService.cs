using TravelAndTours.Web.Models;

namespace TravelAndTours.Web.Services;

public interface IProductsService
{
    Task<List<ProductDto>> GetAllAsync(CancellationToken ct = default);
    Task<ProductDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task CreateAsync(ProductUpsertViewModel vm, CancellationToken ct = default);
    Task UpdateAsync(ProductUpsertViewModel vm, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
