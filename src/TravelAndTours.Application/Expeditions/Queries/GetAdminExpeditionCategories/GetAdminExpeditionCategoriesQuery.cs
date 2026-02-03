using MediatR;
using TravelAndTours.Application.Expeditions.Models;

namespace TravelAndTours.Application.Expeditions.Queries.GetAdminExpeditionCategories;

public sealed record GetAdminExpeditionCategoriesQuery : IRequest<IReadOnlyList<ExpeditionCategoryDto>>;
