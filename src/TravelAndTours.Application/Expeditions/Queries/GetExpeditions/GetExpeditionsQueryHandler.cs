using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Domain.Enums;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Queries.GetExpeditions;

public sealed class GetExpeditionsQueryHandler : IRequestHandler<GetExpeditionsQuery, PagedResult<ExpeditionCardDto>>
{
    private readonly IUnitOfWork _uow;

    public GetExpeditionsQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<PagedResult<ExpeditionCardDto>> Handle(GetExpeditionsQuery request, CancellationToken ct)
    {
        var query = _uow.Expeditions.Query()
            .Where(x => x.Status == ExpeditionStatus.Published);

        if (!string.IsNullOrWhiteSpace(request.CategorySlug))
        {
            var slug = request.CategorySlug.Trim();
            query = query.Where(x => x.Category != null && x.Category.Slug == slug);
        }

        query = ApplySorting(query, request.SortBy, request.SortDirection);

        var totalCount = query.Count();
        var totalPages = totalCount == 0
            ? 0
            : (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var items = query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new ExpeditionCardDto(
                x.Id,
                x.Title,
                x.Slug,
                x.ShortTitle,
                x.Tagline,
                x.DurationDays,
                x.MaxAltitudeMeters,
                x.Difficulty.ToString(),
                x.Region,
                x.Country,
                x.BestSeason,
                x.Category != null ? x.Category.Slug : string.Empty,
                x.MediaAssets
                    .Where(m => m.MediaType == MediaType.Image)
                    .OrderBy(m => m.SortOrder)
                    .Select(m => m.Url)
                    .FirstOrDefault(),
                x.Variants
                    .OrderBy(v => v.PriceFrom)
                    .Select(v => (decimal?)v.PriceFrom)
                    .FirstOrDefault(),
                x.PublishedAt))
            .ToList();

        var result = new PagedResult<ExpeditionCardDto>(
            items,
            request.Page,
            request.PageSize,
            totalCount,
            totalPages);

        return Task.FromResult(result);
    }

    private static IQueryable<Domain.Entities.Expedition> ApplySorting(
        IQueryable<Domain.Entities.Expedition> query,
        string? sortBy,
        string? sortDirection)
    {
        var descending = string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase);
        var key = sortBy?.Trim().ToLowerInvariant();

        return key switch
        {
            "title" => descending
                ? query.OrderByDescending(x => x.Title)
                : query.OrderBy(x => x.Title),
            "published" or "publishedat" => descending
                ? query.OrderByDescending(x => x.PublishedAt)
                : query.OrderBy(x => x.PublishedAt),
            _ => query.OrderByDescending(x => x.PublishedAt)
        };
    }
}
