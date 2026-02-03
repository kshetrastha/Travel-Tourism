using MediatR;
using TravelAndTours.Application.Common.Errors;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Domain.Enums;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Queries.GetFixedDepartures;

public sealed class GetFixedDeparturesQueryHandler : IRequestHandler<GetFixedDeparturesQuery, IReadOnlyList<FixedDepartureDto>>
{
    private readonly IUnitOfWork _uow;

    public GetFixedDeparturesQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<IReadOnlyList<FixedDepartureDto>> Handle(GetFixedDeparturesQuery request, CancellationToken ct)
    {
        var slug = request.Slug.Trim();

        var departures = _uow.Expeditions.Query()
            .Where(x => x.Status == ExpeditionStatus.Published && x.Type == request.Type && x.Slug == slug)
            .SelectMany(x => x.FixedDepartures)
            .OrderBy(fd => fd.StartDate)
            .Select(fd => new FixedDepartureDto(
                fd.Id,
                fd.StartDate,
                fd.EndDate,
                fd.Price,
                fd.Currency,
                fd.SlotsTotal,
                fd.SlotsAvailable,
                fd.Status.ToString(),
                fd.Notes,
                fd.VariantId))
            .ToList();

        if (departures.Count == 0)
        {
            var exists = _uow.Expeditions.Query().Any(x => x.Status == ExpeditionStatus.Published && x.Type == request.Type && x.Slug == slug);
            if (!exists)
            {
                throw new NotFoundException("Expedition not found.");
            }
        }

        return Task.FromResult<IReadOnlyList<FixedDepartureDto>>(departures);
    }
}
