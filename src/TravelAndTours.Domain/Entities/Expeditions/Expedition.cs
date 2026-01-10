using TravelAndTours.Domain.Common;
using TravelAndTours.Domain.Enums;

namespace TravelAndTours.Domain.Entities;

public sealed class Expedition : BaseEntity
{
    public int CategoryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? ShortTitle { get; set; }
    public string? Tagline { get; set; }
    public int DurationDays { get; set; }
    public int MaxAltitudeMeters { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    public string Region { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string BestSeason { get; set; } = string.Empty;
    public int? GroupSizeMin { get; set; }
    public int? GroupSizeMax { get; set; }
    public string? StartingPoint { get; set; }
    public string? EndingPoint { get; set; }
    public string OverviewMarkdown { get; set; } = string.Empty;
    public string IncludesMarkdown { get; set; } = string.Empty;
    public string ExcludesMarkdown { get; set; } = string.Empty;
    public ExpeditionStatus Status { get; set; } = ExpeditionStatus.Draft;
    public DateTime? PublishedAt { get; set; }

    public ExpeditionCategory? Category { get; set; }
    public ICollection<ExpeditionVariant> Variants { get; set; } = new List<ExpeditionVariant>();
    public ICollection<ExpeditionFact> Facts { get; set; } = new List<ExpeditionFact>();
    public ICollection<ItineraryDay> ItineraryDays { get; set; } = new List<ItineraryDay>();
    public ICollection<FixedDeparture> FixedDepartures { get; set; } = new List<FixedDeparture>();
    public ICollection<MediaAsset> MediaAssets { get; set; } = new List<MediaAsset>();
    public ICollection<FaqItem> FaqItems { get; set; } = new List<FaqItem>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
