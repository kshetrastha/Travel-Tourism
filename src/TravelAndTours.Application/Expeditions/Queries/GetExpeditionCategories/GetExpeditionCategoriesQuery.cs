using MediatR;
using TravelAndTours.Application.Expeditions.Models;

namespace TravelAndTours.Application.Expeditions.Queries.GetExpeditionCategories;

public sealed record GetExpeditionCategoriesQuery : IRequest<IReadOnlyList<ExpeditionCategoryDto>>;
