using TravelAndTours.Domain.Entities;

namespace TravelAndTours.Domain.Interfaces;

public interface IExpeditionRepository : IRepository<Expedition>
{
    Task<bool> ExistsBySlugAsync(string slug, CancellationToken ct);
    Task<Expedition?> GetBySlugAsync(string slug, CancellationToken ct);
}
