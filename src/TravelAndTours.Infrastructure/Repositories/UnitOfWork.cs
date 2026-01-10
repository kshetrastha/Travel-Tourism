using Microsoft.EntityFrameworkCore.Storage;
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
    }

    public IProductRepository Products { get; }

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
