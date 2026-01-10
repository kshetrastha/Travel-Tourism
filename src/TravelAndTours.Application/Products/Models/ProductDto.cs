namespace TravelAndTours.Application.Products.Models;

public sealed record ProductDto(
    int Id,
    string Name,
    decimal Price,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
