using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TravelAndTours.Application.Common.Mapping;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Products.Models;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Products.Queries.GetProducts;

public sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PagedResult<ProductDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<GetProductsQueryHandler> _logger;

    public GetProductsQueryHandler(IUnitOfWork uow, ILogger<GetProductsQueryHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<PagedResult<ProductDto>> Handle(GetProductsQuery request, CancellationToken ct)
    {
        var page = Math.Max(1, request.Page);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);

        // Query() returns IQueryable so Infrastructure can provide EF Core provider
        var baseQuery = _uow.Products.Query();
        var totalCount = await baseQuery.LongCountAsync(ct);
        var totalPages = totalCount == 0
            ? 0
            : (int)Math.Ceiling(totalCount / (double)pageSize);

        var query = baseQuery
            .OrderByDescending(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        // Materialize in Infrastructure provider
        // We keep handler provider-agnostic; repo Query() decides how it executes.
        var list = await query.ToListAsync(ct); // For EF Core this is executed in Infrastructure assembly
        _logger.LogInformation("Fetched {Count} products out of {TotalCount}.", list.Count, totalCount);

        var items = list.Select(p => p.ToDto()).ToList();

        return new PagedResult<ProductDto>(
            items,
            page,
            pageSize,
            totalCount,
            totalPages,
            page < totalPages,
            page > 1);
    }
}
