using Microsoft.EntityFrameworkCore;
using TravelAndTours.Domain.Interfaces;
using TravelAndTours.Infrastructure.Persistence;

namespace TravelAndTours.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext Db;

    public Repository(AppDbContext db)
    {
        Db = db;
    }

    public virtual async Task<T?> GetByIdAsync(int id, CancellationToken ct) =>
        await Db.Set<T>().FindAsync([id], ct);

    public virtual IQueryable<T> Query() => Db.Set<T>().AsQueryable();

    public virtual async Task AddAsync(T entity, CancellationToken ct) =>
        await Db.Set<T>().AddAsync(entity, ct);

    public virtual void Update(T entity) => Db.Set<T>().Update(entity);

    public virtual void Remove(T entity) => Db.Set<T>().Remove(entity);
}
