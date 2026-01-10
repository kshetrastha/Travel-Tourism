using TravelAndTours.Domain.Common;
using TravelAndTours.Domain.Enums;

namespace TravelAndTours.Domain.Entities;

public sealed class MediaAsset : BaseEntity
{
    public int ExpeditionId { get; set; }
    public MediaType MediaType { get; set; }
    public string Url { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public string? Title { get; set; }
    public int SortOrder { get; set; }

    public Expedition? Expedition { get; set; }
}
