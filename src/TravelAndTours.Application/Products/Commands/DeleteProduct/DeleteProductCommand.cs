using MediatR;

namespace TravelAndTours.Application.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(int Id) : IRequest<bool>;
