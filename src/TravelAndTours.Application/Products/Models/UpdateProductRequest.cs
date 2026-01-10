namespace TravelAndTours.Application.Products.Models;

public sealed record UpdateProductRequest(
    string Name,
    decimal Price
);
