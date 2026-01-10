using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TravelAndTours.Domain.Entities;
using TravelAndTours.Domain.Enums;

namespace TravelAndTours.Infrastructure.Persistence;

public static class ExpeditionSeeder
{
    public static async Task SeedAsync(IServiceProvider services, CancellationToken ct)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (await db.ExpeditionCategories.AnyAsync(ct))
        {
            return;
        }

        var categories = new List<ExpeditionCategory>
        {
            new() { Name = "VIP & Luxury Services", Slug = "vip-luxury-services", SortOrder = 1, IsActive = true, CreatedAt = DateTime.UtcNow },
            new() { Name = "Everest Expeditions", Slug = "everest-expeditions", SortOrder = 2, IsActive = true, CreatedAt = DateTime.UtcNow },
            new() { Name = "8000ers", Slug = "8000ers", SortOrder = 3, IsActive = true, CreatedAt = DateTime.UtcNow },
            new() { Name = "7000ers", Slug = "7000ers", SortOrder = 4, IsActive = true, CreatedAt = DateTime.UtcNow },
            new() { Name = "6000ers", Slug = "6000ers", SortOrder = 5, IsActive = true, CreatedAt = DateTime.UtcNow },
            new() { Name = "7 summits", Slug = "7-summits", SortOrder = 6, IsActive = true, CreatedAt = DateTime.UtcNow },
            new() { Name = "Double 8000ers", Slug = "double-8000ers", SortOrder = 7, IsActive = true, CreatedAt = DateTime.UtcNow }
        };

        var category8000ers = categories.First(c => c.Slug == "8000ers");

        var expedition = new Expedition
        {
            Title = "Everest & Lhotse double 8000m Expedition 2026",
            Slug = "everest-lhotse-double-8000ers",
            ShortTitle = "Everest + Lhotse Double",
            Tagline = "Two legendary peaks in one elite expedition season",
            Category = category8000ers,
            DurationDays = 45,
            MaxAltitudeMeters = 8849,
            Difficulty = DifficultyLevel.Extreme,
            Region = "Khumbu",
            Country = "Nepal",
            BestSeason = "April - May",
            GroupSizeMin = 4,
            GroupSizeMax = 12,
            StartingPoint = "Kathmandu",
            EndingPoint = "Kathmandu",
            OverviewMarkdown = "Experience the ultimate Himalayan challenge with a combined Everest and Lhotse expedition. This program blends summit pushes, acclimatization rotations, and premium logistics.",
            IncludesMarkdown = "- Airport transfers and city logistics\n- Domestic flights to Lukla\n- Base camp accommodation and meals\n- Climbing permits and liaison officer\n- High-altitude Sherpa support",
            ExcludesMarkdown = "- International airfare\n- Personal climbing gear\n- Travel insurance\n- Tips and personal expenses",
            Status = ExpeditionStatus.Published,
            PublishedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        expedition.Variants.Add(new ExpeditionVariant
        {
            VariantType = ExpeditionVariantType.Standard,
            TitleOverride = "Standard Expedition",
            PriceFrom = 65000m,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        });

        expedition.Variants.Add(new ExpeditionVariant
        {
            VariantType = ExpeditionVariantType.Vip,
            TitleOverride = "VIP Expedition",
            PriceFrom = 85000m,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        });

        expedition.Facts =
        [
            new ExpeditionFact { Label = "Duration", Value = "45 Days", SortOrder = 1, CreatedAt = DateTime.UtcNow },
            new ExpeditionFact { Label = "Max Altitude", Value = "8,849m", SortOrder = 2, CreatedAt = DateTime.UtcNow },
            new ExpeditionFact { Label = "Difficulty", Value = "Extreme", SortOrder = 3, CreatedAt = DateTime.UtcNow },
            new ExpeditionFact { Label = "Region", Value = "Khumbu, Nepal", SortOrder = 4, CreatedAt = DateTime.UtcNow },
            new ExpeditionFact { Label = "Best Season", Value = "April - May", SortOrder = 5, CreatedAt = DateTime.UtcNow },
            new ExpeditionFact { Label = "Group Size", Value = "4-12 climbers", SortOrder = 6, CreatedAt = DateTime.UtcNow }
        ];

        expedition.ItineraryDays =
        [
            new ItineraryDay
            {
                DayNumber = 1,
                Title = "Arrival in Kathmandu",
                DescriptionMarkdown = "Meet the expedition team, gear check, and briefing.",
                Meals = "Welcome dinner",
                Accommodation = "5-star hotel",
                CreatedAt = DateTime.UtcNow
            },
            new ItineraryDay
            {
                DayNumber = 12,
                Title = "Everest Base Camp",
                DescriptionMarkdown = "Trek to Everest Base Camp and establish camp operations.",
                Meals = "Breakfast/Lunch/Dinner",
                Accommodation = "Base camp",
                ElevationMeters = 5364,
                CreatedAt = DateTime.UtcNow
            },
            new ItineraryDay
            {
                DayNumber = 40,
                Title = "Summit Push",
                DescriptionMarkdown = "Summit Everest followed by Lhotse contingent on weather window.",
                Meals = "High-altitude rations",
                Accommodation = "High camp",
                ElevationMeters = 8849,
                CreatedAt = DateTime.UtcNow
            }
        ];

        expedition.FixedDepartures =
        [
            new FixedDeparture
            {
                StartDate = new DateTime(DateTime.UtcNow.Year, 4, 5),
                EndDate = new DateTime(DateTime.UtcNow.Year, 5, 20),
                Price = 65000m,
                Currency = "USD",
                SlotsTotal = 12,
                SlotsAvailable = 8,
                Status = FixedDepartureStatus.Open,
                Notes = "Guaranteed departure with minimum 6 climbers",
                CreatedAt = DateTime.UtcNow
            },
            new FixedDeparture
            {
                StartDate = new DateTime(DateTime.UtcNow.Year, 4, 12),
                EndDate = new DateTime(DateTime.UtcNow.Year, 5, 27),
                Price = 85000m,
                Currency = "USD",
                SlotsTotal = 8,
                SlotsAvailable = 3,
                Status = FixedDepartureStatus.Guaranteed,
                Notes = "VIP climbing team",
                CreatedAt = DateTime.UtcNow
            }
        ];

        expedition.MediaAssets =
        [
            new MediaAsset
            {
                MediaType = MediaType.Image,
                Url = "https://images.unsplash.com/photo-1500530855697-b586d89ba3ee",
                ThumbnailUrl = "https://images.unsplash.com/photo-1500530855697-b586d89ba3ee?w=600",
                Title = "Everest Base Camp",
                SortOrder = 1,
                CreatedAt = DateTime.UtcNow
            },
            new MediaAsset
            {
                MediaType = MediaType.Image,
                Url = "https://images.unsplash.com/photo-1469474968028-56623f02e42e",
                ThumbnailUrl = "https://images.unsplash.com/photo-1469474968028-56623f02e42e?w=600",
                Title = "Khumbu Glacier",
                SortOrder = 2,
                CreatedAt = DateTime.UtcNow
            },
            new MediaAsset
            {
                MediaType = MediaType.Video,
                Url = "https://www.youtube.com/embed/aqz-KE-bpKQ",
                ThumbnailUrl = "https://img.youtube.com/vi/aqz-KE-bpKQ/hqdefault.jpg",
                Title = "Expedition Film",
                SortOrder = 3,
                CreatedAt = DateTime.UtcNow
            }
        ];

        expedition.FaqItems =
        [
            new FaqItem
            {
                Question = "Do I need prior 8000m experience?",
                AnswerMarkdown = "Yes. Climbers should have at least one prior 8000m summit and extensive high-altitude experience.",
                SortOrder = 1,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        ];

        expedition.Reviews =
        [
            new Review
            {
                ReviewerName = "Alex P.",
                Rating = 5,
                Comment = "Outstanding logistics and Sherpa team support.",
                IsApproved = true,
                CreatedAt = DateTime.UtcNow
            }
        ];

        await db.ExpeditionCategories.AddRangeAsync(categories, ct);
        await db.Expeditions.AddAsync(expedition, ct);
        await db.SaveChangesAsync(ct);
    }
}
