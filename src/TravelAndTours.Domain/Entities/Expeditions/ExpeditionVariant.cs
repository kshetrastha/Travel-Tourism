using TravelAndTours.Domain.Common;
using TravelAndTours.Domain.Enums;

namespace TravelAndTours.Domain.Entities;

public sealed class ExpeditionVariant : BaseEntity
{
    public int ExpeditionId { get; set; }
    public ExpeditionVariantType VariantType { get; set; }
    public string? TitleOverride { get; set; }
    public decimal PriceFrom { get; set; }
    public string? InclusionsOverrideMarkdown { get; set; }
    public string? ExclusionsOverrideMarkdown { get; set; }
    public bool IsActive { get; set; } = true;

    public Expedition? Expedition { get; set; }
}
