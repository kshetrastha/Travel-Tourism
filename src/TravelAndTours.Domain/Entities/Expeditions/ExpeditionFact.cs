using TravelAndTours.Domain.Common;

namespace TravelAndTours.Domain.Entities;

public sealed class ExpeditionFact : BaseEntity
{
    public int ExpeditionId { get; set; }
    public string Label { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public int SortOrder { get; set; }

    public Expedition? Expedition { get; set; }
}
