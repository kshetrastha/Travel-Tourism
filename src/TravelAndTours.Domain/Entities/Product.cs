using TravelAndTours.Domain.Common;

namespace TravelAndTours.Domain.Entities;

public sealed class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
