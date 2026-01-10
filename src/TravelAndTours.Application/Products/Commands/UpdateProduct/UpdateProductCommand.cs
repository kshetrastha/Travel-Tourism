using MediatR;

namespace TravelAndTours.Application.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(int Id, string Name, decimal Price) : IRequest<bool>;
