using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Queries.GetAdminExpeditions;

public sealed class GetAdminExpeditionsQueryHandler : IRequestHandler<GetAdminExpeditionsQuery, PagedResult<AdminExpeditionSummaryDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAdminExpeditionsQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<PagedResult<AdminExpeditionSummaryDto>> Handle(GetAdminExpeditionsQuery request, CancellationToken ct)
    {
        var page = Math.Max(1, request.Page);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);

        var query = _uow.Expeditions.Query()
            .OrderByDescending(x => x.CreatedAt);

        var totalCount = await query.LongCountAsync(ct);
        var totalPages = totalCount == 0
            ? 0
            : (int)Math.Ceiling(totalCount / (double)pageSize);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new AdminExpeditionSummaryDto(
                x.Id,
                x.Title,
                x.Slug,
                x.Status.ToString(),
                x.Category != null ? x.Category.Name : "Unassigned",
                x.CreatedAt,
                x.UpdatedAt))
            .ToListAsync(ct);

        var result = new PagedResult<AdminExpeditionSummaryDto>(
            items.AsReadOnly(),
            page,
            pageSize,
            totalCount,
            totalPages,
            page < totalPages,
            page > 1);

        return result;
    }
}
