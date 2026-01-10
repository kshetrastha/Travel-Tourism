using Microsoft.EntityFrameworkCore.Storage;
using TravelAndTours.Domain.Entities;
using TravelAndTours.Domain.Interfaces;
using TravelAndTours.Infrastructure.Persistence;

namespace TravelAndTours.Infrastructure.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    private IDbContextTransaction? _tx;

    public UnitOfWork(AppDbContext db)
    {
        _db = db;
        Products = new ProductRepository(db);
        ExpeditionCategories = new ExpeditionCategoryRepository(db);
        Expeditions = new ExpeditionRepository(db);
        ExpeditionVariants = new Repository<ExpeditionVariant>(db);
        ExpeditionFacts = new Repository<ExpeditionFact>(db);
        ItineraryDays = new Repository<ItineraryDay>(db);
        FixedDepartures = new Repository<FixedDeparture>(db);
        MediaAssets = new Repository<MediaAsset>(db);
        FaqItems = new Repository<FaqItem>(db);
        Reviews = new Repository<Review>(db);
    }

    public IProductRepository Products { get; }
    public IExpeditionCategoryRepository ExpeditionCategories { get; }
    public IExpeditionRepository Expeditions { get; }
    public IRepository<ExpeditionVariant> ExpeditionVariants { get; }
    public IRepository<ExpeditionFact> ExpeditionFacts { get; }
    public IRepository<ItineraryDay> ItineraryDays { get; }
    public IRepository<FixedDeparture> FixedDepartures { get; }
    public IRepository<MediaAsset> MediaAssets { get; }
    public IRepository<FaqItem> FaqItems { get; }
    public IRepository<Review> Reviews { get; }

    public Task<int> SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);

    public async Task BeginTransactionAsync(CancellationToken ct)
    {
        if (_tx is not null)
            return;

        _tx = await _db.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitAsync(CancellationToken ct)
    {
        if (_tx is null)
            return;

        await _db.SaveChangesAsync(ct);
        await _tx.CommitAsync(ct);
        await _tx.DisposeAsync();
        _tx = null;
    }

    public async Task RollbackAsync(CancellationToken ct)
    {
        if (_tx is null)
            return;

        await _tx.RollbackAsync(ct);
        await _tx.DisposeAsync();
        _tx = null;
    }
}
