using MediatR;
using Microsoft.Extensions.Logging;
using TravelAndTours.Application.Common.Mapping;
using TravelAndTours.Application.Products.Models;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Products.Queries.GetProducts;

public sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IReadOnlyList<ProductDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<GetProductsQueryHandler> _logger;

    public GetProductsQueryHandler(IUnitOfWork uow, ILogger<GetProductsQueryHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<IReadOnlyList<ProductDto>> Handle(GetProductsQuery request, CancellationToken ct)
    {
        // Query() returns IQueryable so Infrastructure can provide EF Core provider
        var query = _uow.Products.Query()
            .OrderByDescending(x => x.Id)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize);

        // Materialize in Infrastructure provider
        // We keep handler provider-agnostic; repo Query() decides how it executes.
        var list = query.ToList(); // For EF Core this is executed in Infrastructure assembly
        _logger.LogInformation("Fetched {Count} products.", list.Count);

        return list.Select(p => p.ToDto()).ToList();
    }
}
