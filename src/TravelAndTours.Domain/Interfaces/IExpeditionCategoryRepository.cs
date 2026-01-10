using TravelAndTours.Domain.Entities;

namespace TravelAndTours.Domain.Interfaces;

public interface IExpeditionCategoryRepository : IRepository<ExpeditionCategory>
{
    Task<bool> ExistsBySlugAsync(string slug, CancellationToken ct);
    Task<ExpeditionCategory?> GetBySlugAsync(string slug, CancellationToken ct);
}
