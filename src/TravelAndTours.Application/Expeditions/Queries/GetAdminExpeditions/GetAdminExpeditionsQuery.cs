using MediatR;
using TravelAndTours.Application.Expeditions.Models;

namespace TravelAndTours.Application.Expeditions.Queries.GetAdminExpeditions;

public sealed record GetAdminExpeditionsQuery : IRequest<IReadOnlyList<AdminExpeditionSummaryDto>>;
