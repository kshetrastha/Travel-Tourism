using MediatR;
using TravelAndTours.Application.Expeditions.Models;

namespace TravelAndTours.Application.Expeditions.Queries.GetAdminExpeditionDetail;

public sealed record GetAdminExpeditionDetailQuery(int Id) : IRequest<AdminExpeditionDetailDto>;
