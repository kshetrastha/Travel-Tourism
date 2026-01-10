using MediatR;
using TravelAndTours.Application.Common.Errors;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Domain.Enums;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Queries.GetExpeditionDetail;

public sealed class GetExpeditionDetailQueryHandler : IRequestHandler<GetExpeditionDetailQuery, ExpeditionDetailDto>
{
    private readonly IUnitOfWork _uow;

    public GetExpeditionDetailQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<ExpeditionDetailDto> Handle(GetExpeditionDetailQuery request, CancellationToken ct)
    {
        var slug = request.Slug.Trim();

        var detail = _uow.Expeditions.Query()
            .Where(x => x.Status == ExpeditionStatus.Published && x.Slug == slug)
            .Select(x => new ExpeditionDetailDto(
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
                x.GroupSizeMin,
                x.GroupSizeMax,
                x.StartingPoint,
                x.EndingPoint,
                x.OverviewMarkdown,
                x.IncludesMarkdown,
                x.ExcludesMarkdown,
                x.PublishedAt,
                new ExpeditionCategoryDto(
                    x.Category!.Id,
                    x.Category.Name,
                    x.Category.Slug,
                    x.Category.ParentCategoryId,
                    x.Category.SortOrder,
                    x.Category.IsActive),
                x.Facts
                    .OrderBy(f => f.SortOrder)
                    .Select(f => new ExpeditionFactDto(f.Id, f.Label, f.Value, f.SortOrder))
                    .ToList(),
                x.Variants
                    .OrderBy(v => v.PriceFrom)
                    .Select(v => new ExpeditionVariantDto(
                        v.Id,
                        v.VariantType.ToString(),
                        v.TitleOverride,
                        v.PriceFrom,
                        v.IsActive,
                        v.InclusionsOverrideMarkdown,
                        v.ExclusionsOverrideMarkdown))
                    .ToList(),
                x.ItineraryDays
                    .OrderBy(d => d.DayNumber)
                    .Select(d => new ItineraryDayDto(
                        d.Id,
                        d.DayNumber,
                        d.Title,
                        d.DescriptionMarkdown,
                        d.Meals,
                        d.Accommodation,
                        d.ElevationMeters))
                    .ToList(),
                x.FixedDepartures
                    .OrderBy(fd => fd.StartDate)
                    .Select(fd => new FixedDepartureDto(
                        fd.Id,
                        fd.StartDate,
                        fd.EndDate,
                        fd.Price,
                        fd.Currency,
                        fd.SlotsTotal,
                        fd.SlotsAvailable,
                        fd.Status.ToString(),
                        fd.Notes,
                        fd.VariantId))
                    .ToList(),
                x.MediaAssets
                    .OrderBy(m => m.SortOrder)
                    .Select(m => new MediaAssetDto(
                        m.Id,
                        m.MediaType.ToString(),
                        m.Url,
                        m.ThumbnailUrl,
                        m.Title,
                        m.SortOrder))
                    .ToList(),
                x.FaqItems
                    .OrderBy(f => f.SortOrder)
                    .Select(f => new FaqItemDto(
                        f.Id,
                        f.Question,
                        f.AnswerMarkdown,
                        f.SortOrder,
                        f.IsActive))
                    .ToList(),
                x.Reviews
                    .Where(r => r.IsApproved)
                    .OrderByDescending(r => r.CreatedAt)
                    .Select(r => new ReviewDto(
                        r.Id,
                        r.ReviewerName,
                        r.Rating,
                        r.Comment,
                        r.CreatedAt,
                        r.IsApproved))
                    .ToList()))
            .FirstOrDefault();

        if (detail is null)
        {
            throw new NotFoundException("Expedition not found.");
        }

        return Task.FromResult(detail);
    }
}
