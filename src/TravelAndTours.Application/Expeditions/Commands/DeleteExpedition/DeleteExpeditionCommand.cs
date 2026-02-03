using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Domain.Enums;

namespace TravelAndTours.Application.Expeditions.Commands.DeleteExpedition;

public sealed record DeleteExpeditionCommand(
    int Id,
    ExpeditionType Type = ExpeditionType.Expedition) : IRequest<ApiResponse<bool>>;
