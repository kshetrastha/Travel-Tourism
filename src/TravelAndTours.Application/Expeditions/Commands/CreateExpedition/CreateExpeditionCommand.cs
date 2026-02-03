using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Domain.Enums;

namespace TravelAndTours.Application.Expeditions.Commands.CreateExpedition;

public sealed record CreateExpeditionCommand(
    ExpeditionUpsertModel Model,
    ExpeditionType Type = ExpeditionType.Expedition) : IRequest<ApiResponse<int>>;
