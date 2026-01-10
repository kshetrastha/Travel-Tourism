using MediatR;
using TravelAndTours.Application.Common.Models;

namespace TravelAndTours.Application.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(string Name, decimal Price) : IRequest<ApiResponse<int>>;
