using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelAndTours.Domain.Entities;
using TravelAndTours.Domain.Enums;
using TravelAndTours.Infrastructure.Identity;

namespace TravelAndTours.Infrastructure.Persistence;

public sealed class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<ExpeditionCategory> ExpeditionCategories => Set<ExpeditionCategory>();
    public DbSet<Expedition> Expeditions => Set<Expedition>();
    public DbSet<ExpeditionVariant> ExpeditionVariants => Set<ExpeditionVariant>();
    public DbSet<ExpeditionFact> ExpeditionFacts => Set<ExpeditionFact>();
    public DbSet<ItineraryDay> ItineraryDays => Set<ItineraryDay>();
    public DbSet<FixedDeparture> FixedDepartures => Set<FixedDeparture>();
    public DbSet<MediaAsset> MediaAssets => Set<MediaAsset>();
    public DbSet<FaqItem> FaqItems => Set<FaqItem>();
    public DbSet<Review> Reviews => Set<Review>();

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

        builder.Entity<ExpeditionCategory>(b =>
        {
            b.ToTable("ExpeditionCategories");
            b.HasKey(x => x.Id);

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            b.Property(x => x.Slug)
                .IsRequired()
                .HasMaxLength(200);

            b.HasIndex(x => x.Slug)
                .IsUnique();

            b.Property(x => x.SortOrder)
                .HasDefaultValue(0);

            b.Property(x => x.IsActive)
                .HasDefaultValue(true);

            b.Property(x => x.CreatedAt)
                .HasDefaultValueSql("timezone('utc', now())");

            b.Property(x => x.UpdatedAt)
                .IsRequired(false);

            b.HasOne(x => x.ParentCategory)
                .WithMany(x => x.ChildCategories)
                .HasForeignKey(x => x.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Expedition>(b =>
        {
            b.ToTable("Expeditions");
            b.HasKey(x => x.Id);

            b.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(250);

            b.Property(x => x.Slug)
                .IsRequired()
                .HasMaxLength(200);

            b.HasIndex(x => x.Slug)
                .IsUnique();

            b.Property(x => x.ShortTitle)
                .HasMaxLength(120);

            b.Property(x => x.Tagline)
                .HasMaxLength(200);

            b.Property(x => x.Region)
                .HasMaxLength(120);

            b.Property(x => x.Country)
                .HasMaxLength(120);

            b.Property(x => x.BestSeason)
                .HasMaxLength(120);

            b.Property(x => x.StartingPoint)
                .HasMaxLength(120);

            b.Property(x => x.EndingPoint)
                .HasMaxLength(120);

            b.Property(x => x.OverviewMarkdown)
                .HasColumnType("text");

            b.Property(x => x.IncludesMarkdown)
                .HasColumnType("text");

            b.Property(x => x.ExcludesMarkdown)
                .HasColumnType("text");

            b.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasDefaultValue(ExpeditionStatus.Draft);

            b.Property(x => x.Difficulty)
                .HasConversion<string>()
                .HasMaxLength(30);

            b.Property(x => x.CreatedAt)
                .HasDefaultValueSql("timezone('utc', now())");

            b.Property(x => x.UpdatedAt)
                .IsRequired(false);

            b.HasOne(x => x.Category)
                .WithMany(x => x.Expeditions)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ExpeditionVariant>(b =>
        {
            b.ToTable("ExpeditionVariants");
            b.HasKey(x => x.Id);

            b.Property(x => x.VariantType)
                .HasConversion<string>()
                .HasMaxLength(30);

            b.Property(x => x.TitleOverride)
                .HasMaxLength(200);

            b.Property(x => x.PriceFrom)
                .HasColumnType("numeric(18,2)");

            b.Property(x => x.IsActive)
                .HasDefaultValue(true);

            b.Property(x => x.CreatedAt)
                .HasDefaultValueSql("timezone('utc', now())");

            b.Property(x => x.UpdatedAt)
                .IsRequired(false);

            b.HasOne(x => x.Expedition)
                .WithMany(x => x.Variants)
                .HasForeignKey(x => x.ExpeditionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ExpeditionFact>(b =>
        {
            b.ToTable("ExpeditionFacts");
            b.HasKey(x => x.Id);

            b.Property(x => x.Label)
                .IsRequired()
                .HasMaxLength(120);

            b.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(200);

            b.Property(x => x.SortOrder)
                .HasDefaultValue(0);

            b.Property(x => x.CreatedAt)
                .HasDefaultValueSql("timezone('utc', now())");

            b.Property(x => x.UpdatedAt)
                .IsRequired(false);

            b.HasOne(x => x.Expedition)
                .WithMany(x => x.Facts)
                .HasForeignKey(x => x.ExpeditionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ItineraryDay>(b =>
        {
            b.ToTable("ItineraryDays");
            b.HasKey(x => x.Id);

            b.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            b.Property(x => x.DescriptionMarkdown)
                .HasColumnType("text");

            b.Property(x => x.Meals)
                .HasMaxLength(120);

            b.Property(x => x.Accommodation)
                .HasMaxLength(200);

            b.Property(x => x.CreatedAt)
                .HasDefaultValueSql("timezone('utc', now())");

            b.Property(x => x.UpdatedAt)
                .IsRequired(false);

            b.HasOne(x => x.Expedition)
                .WithMany(x => x.ItineraryDays)
                .HasForeignKey(x => x.ExpeditionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<FixedDeparture>(b =>
        {
            b.ToTable("FixedDepartures");
            b.HasKey(x => x.Id);

            b.Property(x => x.Price)
                .HasColumnType("numeric(18,2)");

            b.Property(x => x.Currency)
                .IsRequired()
                .HasMaxLength(3);

            b.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(30)
                .HasDefaultValue(FixedDepartureStatus.Open);

            b.Property(x => x.Notes)
                .HasMaxLength(500);

            b.Property(x => x.CreatedAt)
                .HasDefaultValueSql("timezone('utc', now())");

            b.Property(x => x.UpdatedAt)
                .IsRequired(false);

            b.HasOne(x => x.Expedition)
                .WithMany(x => x.FixedDepartures)
                .HasForeignKey(x => x.ExpeditionId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Variant)
                .WithMany()
                .HasForeignKey(x => x.VariantId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<MediaAsset>(b =>
        {
            b.ToTable("MediaAssets");
            b.HasKey(x => x.Id);

            b.Property(x => x.MediaType)
                .HasConversion<string>()
                .HasMaxLength(20);

            b.Property(x => x.Url)
                .IsRequired()
                .HasMaxLength(500);

            b.Property(x => x.ThumbnailUrl)
                .HasMaxLength(500);

            b.Property(x => x.Title)
                .HasMaxLength(200);

            b.Property(x => x.SortOrder)
                .HasDefaultValue(0);

            b.Property(x => x.CreatedAt)
                .HasDefaultValueSql("timezone('utc', now())");

            b.Property(x => x.UpdatedAt)
                .IsRequired(false);

            b.HasOne(x => x.Expedition)
                .WithMany(x => x.MediaAssets)
                .HasForeignKey(x => x.ExpeditionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<FaqItem>(b =>
        {
            b.ToTable("FaqItems");
            b.HasKey(x => x.Id);

            b.Property(x => x.Question)
                .IsRequired()
                .HasMaxLength(300);

            b.Property(x => x.AnswerMarkdown)
                .HasColumnType("text");

            b.Property(x => x.SortOrder)
                .HasDefaultValue(0);

            b.Property(x => x.IsActive)
                .HasDefaultValue(true);

            b.Property(x => x.CreatedAt)
                .HasDefaultValueSql("timezone('utc', now())");

            b.Property(x => x.UpdatedAt)
                .IsRequired(false);

            b.HasOne(x => x.Expedition)
                .WithMany(x => x.FaqItems)
                .HasForeignKey(x => x.ExpeditionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Review>(b =>
        {
            b.ToTable("Reviews");
            b.HasKey(x => x.Id);

            b.Property(x => x.ReviewerName)
                .HasMaxLength(120);

            b.Property(x => x.Comment)
                .HasColumnType("text");

            b.Property(x => x.IsApproved)
                .HasDefaultValue(false);

            b.Property(x => x.CreatedAt)
                .HasDefaultValueSql("timezone('utc', now())");

            b.Property(x => x.UpdatedAt)
                .IsRequired(false);

            b.HasOne(x => x.Expedition)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.ExpeditionId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
