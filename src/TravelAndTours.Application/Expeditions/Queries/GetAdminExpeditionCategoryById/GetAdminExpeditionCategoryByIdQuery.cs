using MediatR;
using TravelAndTours.Application.Expeditions.Models;

namespace TravelAndTours.Application.Expeditions.Queries.GetAdminExpeditionCategoryById;

public sealed record GetAdminExpeditionCategoryByIdQuery(int Id) : IRequest<ExpeditionCategoryDto?>;
