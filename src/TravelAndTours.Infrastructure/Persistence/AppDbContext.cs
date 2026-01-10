using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelAndTours.Domain.Entities;
using TravelAndTours.Infrastructure.Identity;

namespace TravelAndTours.Infrastructure.Persistence;

public sealed class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Product>(b =>
        {
            b.ToTable("Products");
            b.HasKey(x => x.Id);

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            b.Property(x => x.Price)
                .HasColumnType("numeric(18,2)");

            // Postgres UTC default
            b.Property(x => x.CreatedAt)
                .HasDefaultValueSql("timezone('utc', now())");

            b.Property(x => x.UpdatedAt)
                .IsRequired(false);
        });
    }
}
