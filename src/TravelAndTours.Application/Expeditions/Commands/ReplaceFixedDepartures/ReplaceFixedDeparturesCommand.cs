using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Domain.Enums;

namespace TravelAndTours.Application.Expeditions.Commands.ReplaceFixedDepartures;

public sealed record ReplaceFixedDeparturesCommand(
    int ExpeditionId,
    IReadOnlyList<FixedDepartureInput> Departures,
    ExpeditionType Type = ExpeditionType.Expedition) : IRequest<ApiResponse<bool>>;
