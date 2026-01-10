using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Products.Models;

namespace TravelAndTours.Application.Products.Queries.GetProducts;

public sealed record GetProductsQuery(int Page = 1, int PageSize = 50) : IRequest<PagedResult<ProductDto>>;
