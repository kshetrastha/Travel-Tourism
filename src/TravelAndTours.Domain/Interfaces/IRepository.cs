namespace TravelAndTours.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id, CancellationToken ct);
    IQueryable<T> Query();
    Task AddAsync(T entity, CancellationToken ct);
    void Update(T entity);
    void Remove(T entity);
}
