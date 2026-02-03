using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Domain.Enums;

namespace TravelAndTours.Application.Expeditions.Commands.UpdateExpedition;

public sealed record UpdateExpeditionCommand(
    int Id,
    ExpeditionUpsertModel Model,
    ExpeditionType Type = ExpeditionType.Expedition) : IRequest<ApiResponse<bool>>;
