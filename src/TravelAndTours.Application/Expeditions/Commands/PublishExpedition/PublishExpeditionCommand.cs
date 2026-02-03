using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Domain.Enums;

namespace TravelAndTours.Application.Expeditions.Commands.PublishExpedition;

public sealed record PublishExpeditionCommand(
    int Id,
    ExpeditionType Type = ExpeditionType.Expedition) : IRequest<ApiResponse<bool>>;
