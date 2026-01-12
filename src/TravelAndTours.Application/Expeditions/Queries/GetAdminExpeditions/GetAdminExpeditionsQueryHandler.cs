using MediatR;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Queries.GetAdminExpeditions;

public sealed class GetAdminExpeditionsQueryHandler : IRequestHandler<GetAdminExpeditionsQuery, IReadOnlyList<AdminExpeditionSummaryDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAdminExpeditionsQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<IReadOnlyList<AdminExpeditionSummaryDto>> Handle(GetAdminExpeditionsQuery request, CancellationToken ct)
    {
        var items = _uow.Expeditions.Query()
            .OrderByDescending(x => x.CreatedAt)
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

        return Task.FromResult<IReadOnlyList<AdminExpeditionSummaryDto>>(items);
    }
}
