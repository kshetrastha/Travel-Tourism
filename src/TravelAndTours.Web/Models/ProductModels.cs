using System.ComponentModel.DataAnnotations;

namespace TravelAndTours.Web.Models;

public sealed class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
}

public sealed class ProductUpsertViewModel
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Name { get; set; } = "";

    [Required]
    [Range(0.01, 1000000000)]
    public decimal Price { get; set; }
}
