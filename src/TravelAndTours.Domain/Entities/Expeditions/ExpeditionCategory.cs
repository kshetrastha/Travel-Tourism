using TravelAndTours.Domain.Common;

namespace TravelAndTours.Domain.Entities;

public sealed class ExpeditionCategory : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int? ParentCategoryId { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;

    public ExpeditionCategory? ParentCategory { get; set; }
    public ICollection<ExpeditionCategory> ChildCategories { get; set; } = new List<ExpeditionCategory>();
    public ICollection<Expedition> Expeditions { get; set; } = new List<Expedition>();
}
