using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Expeditions.Models;

namespace TravelAndTours.Application.Expeditions.Commands.ReplaceItinerary;

public sealed record ReplaceItineraryCommand(int ExpeditionId, IReadOnlyList<ItineraryDayInput> Days) : IRequest<ApiResponse<bool>>;
