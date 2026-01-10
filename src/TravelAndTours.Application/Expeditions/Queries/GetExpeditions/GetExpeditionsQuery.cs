using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Expeditions.Models;

namespace TravelAndTours.Application.Expeditions.Queries.GetExpeditions;

public sealed record GetExpeditionsQuery(
    string? CategorySlug,
    int Page = 1,
    int PageSize = 12,
    string? SortBy = null,
    string? SortDirection = null) : IRequest<PagedResult<ExpeditionCardDto>>;
