using TravelAndTours.Domain.Entities;

namespace TravelAndTours.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    // Add product-specific methods here if needed
    Task<bool> ExistsByNameAsync(string name, CancellationToken ct);
}
