using MediatR;

namespace TravelAndTours.Application.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(string Name, decimal Price) : IRequest<int>;
