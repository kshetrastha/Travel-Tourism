using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Domain.Enums;

namespace TravelAndTours.Application.Expeditions.Commands.ReplaceItinerary;

public sealed record ReplaceItineraryCommand(
    int ExpeditionId,
    IReadOnlyList<ItineraryDayInput> Days,
    ExpeditionType Type = ExpeditionType.Expedition) : IRequest<ApiResponse<bool>>;
