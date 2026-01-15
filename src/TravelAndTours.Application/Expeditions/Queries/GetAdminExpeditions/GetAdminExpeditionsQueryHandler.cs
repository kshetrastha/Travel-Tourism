using MediatR;
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

    public Task<PagedResult<AdminExpeditionSummaryDto>> Handle(GetAdminExpeditionsQuery request, CancellationToken ct)
    {
        var query = _uow.Expeditions.Query()
            .OrderByDescending(x => x.CreatedAt);

        var totalCount = query.Count();
        var totalPages = totalCount == 0
            ? 0
            : (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var items = query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new AdminExpeditionSummaryDto(
                x.Id,
                x.Title,
                x.Slug,
                x.Status.ToString(),
                x.Category != null ? x.Category.Name : "Unassigned",
                x.CreatedAt,
                x.UpdatedAt))
            .ToList()
            .AsReadOnly();

        var result = new PagedResult<AdminExpeditionSummaryDto>(
            items,
            request.Page,
            request.PageSize,
            totalCount,
            totalPages);

        return Task.FromResult(result);
    }
}
