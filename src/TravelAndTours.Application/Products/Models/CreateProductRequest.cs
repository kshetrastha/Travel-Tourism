namespace TravelAndTours.Application.Products.Models;

public sealed record CreateProductRequest(
    string Name,
    decimal Price
);
