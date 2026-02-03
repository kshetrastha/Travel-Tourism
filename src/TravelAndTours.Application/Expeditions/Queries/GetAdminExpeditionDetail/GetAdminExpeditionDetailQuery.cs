using MediatR;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Domain.Enums;

namespace TravelAndTours.Application.Expeditions.Queries.GetAdminExpeditionDetail;

public sealed record GetAdminExpeditionDetailQuery(
    int Id,
    ExpeditionType Type = ExpeditionType.Expedition) : IRequest<AdminExpeditionDetailDto>;
