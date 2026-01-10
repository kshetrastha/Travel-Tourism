using TravelAndTours.Domain.Common;

namespace TravelAndTours.Domain.Entities;

public sealed class Review : BaseEntity
{
    public int ExpeditionId { get; set; }
    public string? ReviewerName { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public bool IsApproved { get; set; }

    public Expedition? Expedition { get; set; }
}
