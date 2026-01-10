using MediatR;
using TravelAndTours.Application.Products.Models;

namespace TravelAndTours.Application.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(int Id) : IRequest<ProductDto>;
