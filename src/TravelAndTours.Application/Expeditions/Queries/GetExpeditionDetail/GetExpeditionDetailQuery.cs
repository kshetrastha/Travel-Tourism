using MediatR;
using TravelAndTours.Application.Expeditions.Models;

namespace TravelAndTours.Application.Expeditions.Queries.GetExpeditionDetail;

public sealed record GetExpeditionDetailQuery(string Slug) : IRequest<ExpeditionDetailDto>;
