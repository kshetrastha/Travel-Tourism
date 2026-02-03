using MediatR;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Domain.Enums;

namespace TravelAndTours.Application.Expeditions.Queries.GetFixedDepartures;

public sealed record GetFixedDeparturesQuery(
    string Slug,
    ExpeditionType Type = ExpeditionType.Expedition) : IRequest<IReadOnlyList<FixedDepartureDto>>;
