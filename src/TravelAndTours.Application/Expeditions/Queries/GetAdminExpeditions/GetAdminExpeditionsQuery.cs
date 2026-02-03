using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Domain.Enums;

namespace TravelAndTours.Application.Expeditions.Queries.GetAdminExpeditions;

public sealed record GetAdminExpeditionsQuery(
    int Page = 1,
    int PageSize = 10,
    ExpeditionType Type = ExpeditionType.Expedition) : IRequest<PagedResult<AdminExpeditionSummaryDto>>;
