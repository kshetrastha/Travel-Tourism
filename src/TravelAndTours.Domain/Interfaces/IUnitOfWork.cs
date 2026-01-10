namespace TravelAndTours.Domain.Interfaces;

public interface IUnitOfWork
{
    IProductRepository Products { get; }

    Task<int> SaveChangesAsync(CancellationToken ct);

    Task BeginTransactionAsync(CancellationToken ct);
    Task CommitAsync(CancellationToken ct);
    Task RollbackAsync(CancellationToken ct);
}
