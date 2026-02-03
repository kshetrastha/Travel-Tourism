using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Domain.Enums;

namespace TravelAndTours.Application.Expeditions.Commands.UnpublishExpedition;

public sealed record UnpublishExpeditionCommand(
    int Id,
    ExpeditionType Type = ExpeditionType.Expedition) : IRequest<ApiResponse<bool>>;
