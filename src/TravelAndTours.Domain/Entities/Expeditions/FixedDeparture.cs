using TravelAndTours.Domain.Common;
using TravelAndTours.Domain.Enums;

namespace TravelAndTours.Domain.Entities;

public sealed class FixedDeparture : BaseEntity
{
    public int ExpeditionId { get; set; }
    public int? VariantId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; } = "USD";
    public int SlotsTotal { get; set; }
    public int SlotsAvailable { get; set; }
    public FixedDepartureStatus Status { get; set; }
    public string? Notes { get; set; }

    public Expedition? Expedition { get; set; }
    public ExpeditionVariant? Variant { get; set; }
}
