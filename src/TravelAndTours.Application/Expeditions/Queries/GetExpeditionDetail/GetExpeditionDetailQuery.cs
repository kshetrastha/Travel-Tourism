using MediatR;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Domain.Enums;

namespace TravelAndTours.Application.Expeditions.Queries.GetExpeditionDetail;

public sealed record GetExpeditionDetailQuery(
    string Slug,
    ExpeditionType Type = ExpeditionType.Expedition) : IRequest<ExpeditionDetailDto>;
