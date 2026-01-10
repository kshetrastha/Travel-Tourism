using TravelAndTours.Domain.Entities;

namespace TravelAndTours.Domain.Interfaces;

public interface IUnitOfWork
{
    IProductRepository Products { get; }
    IExpeditionCategoryRepository ExpeditionCategories { get; }
    IExpeditionRepository Expeditions { get; }
    IRepository<ExpeditionVariant> ExpeditionVariants { get; }
    IRepository<ExpeditionFact> ExpeditionFacts { get; }
    IRepository<ItineraryDay> ItineraryDays { get; }
    IRepository<FixedDeparture> FixedDepartures { get; }
    IRepository<MediaAsset> MediaAssets { get; }
    IRepository<FaqItem> FaqItems { get; }
    IRepository<Review> Reviews { get; }

    Task<int> SaveChangesAsync(CancellationToken ct);

    Task BeginTransactionAsync(CancellationToken ct);
    Task CommitAsync(CancellationToken ct);
    Task RollbackAsync(CancellationToken ct);
}
