using Microsoft.EntityFrameworkCore;
using TravelAndTours.Domain.Entities;
using TravelAndTours.Domain.Interfaces;
using TravelAndTours.Infrastructure.Persistence;

namespace TravelAndTours.Infrastructure.Repositories;

public sealed class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext db) : base(db)
    {
    }

    public Task<bool> ExistsByNameAsync(string name, CancellationToken ct)
    {
        var normalized = name.Trim();
        return Db.Products.AnyAsync(p => p.Name == normalized, ct);
    }
}
