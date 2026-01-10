using TravelAndTours.Domain.Common;

namespace TravelAndTours.Domain.Entities;

public sealed class FaqItem : BaseEntity
{
    public int ExpeditionId { get; set; }
    public string Question { get; set; } = string.Empty;
    public string AnswerMarkdown { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;

    public Expedition? Expedition { get; set; }
}
