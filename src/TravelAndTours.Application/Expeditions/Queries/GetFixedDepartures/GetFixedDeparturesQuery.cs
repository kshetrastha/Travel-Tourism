using MediatR;
using TravelAndTours.Application.Expeditions.Models;

namespace TravelAndTours.Application.Expeditions.Queries.GetFixedDepartures;

public sealed record GetFixedDeparturesQuery(string Slug) : IRequest<IReadOnlyList<FixedDepartureDto>>;
