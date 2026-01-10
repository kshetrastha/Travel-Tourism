using Microsoft.EntityFrameworkCore;
using TravelAndTours.Domain.Entities;
using TravelAndTours.Domain.Interfaces;
using TravelAndTours.Infrastructure.Persistence;

namespace TravelAndTours.Infrastructure.Repositories;

public sealed class ExpeditionRepository : Repository<Expedition>, IExpeditionRepository
{
    public ExpeditionRepository(AppDbContext db) : base(db)
    {
    }

    public Task<bool> ExistsBySlugAsync(string slug, CancellationToken ct)
        => Db.Expeditions.AnyAsync(e => e.Slug == slug, ct);

    public Task<Expedition?> GetBySlugAsync(string slug, CancellationToken ct)
        => Db.Expeditions.FirstOrDefaultAsync(e => e.Slug == slug, ct);
}
