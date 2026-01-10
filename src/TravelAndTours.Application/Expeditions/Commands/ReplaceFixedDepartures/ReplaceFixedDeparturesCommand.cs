using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Expeditions.Models;

namespace TravelAndTours.Application.Expeditions.Commands.ReplaceFixedDepartures;

public sealed record ReplaceFixedDeparturesCommand(int ExpeditionId, IReadOnlyList<FixedDepartureInput> Departures) : IRequest<ApiResponse<bool>>;
