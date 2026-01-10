using Microsoft.EntityFrameworkCore;
using TravelAndTours.Domain.Entities;
using TravelAndTours.Domain.Interfaces;
using TravelAndTours.Infrastructure.Persistence;

namespace TravelAndTours.Infrastructure.Repositories;

public sealed class ExpeditionCategoryRepository : Repository<ExpeditionCategory>, IExpeditionCategoryRepository
{
    public ExpeditionCategoryRepository(AppDbContext db) : base(db)
    {
    }

    public Task<bool> ExistsBySlugAsync(string slug, CancellationToken ct)
        => Db.ExpeditionCategories.AnyAsync(c => c.Slug == slug, ct);

    public Task<ExpeditionCategory?> GetBySlugAsync(string slug, CancellationToken ct)
        => Db.ExpeditionCategories.FirstOrDefaultAsync(c => c.Slug == slug, ct);
}
