using TravelAndTours.Domain.Common;

namespace TravelAndTours.Domain.Entities;

public sealed class ItineraryDay : BaseEntity
{
    public int ExpeditionId { get; set; }
    public int DayNumber { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DescriptionMarkdown { get; set; } = string.Empty;
    public string? Meals { get; set; }
    public string? Accommodation { get; set; }
    public int? ElevationMeters { get; set; }

    public Expedition? Expedition { get; set; }
}
