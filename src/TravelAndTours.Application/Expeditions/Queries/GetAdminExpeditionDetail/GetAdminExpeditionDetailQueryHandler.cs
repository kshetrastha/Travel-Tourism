using MediatR;
using TravelAndTours.Application.Common.Errors;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Queries.GetAdminExpeditionDetail;

public sealed class GetAdminExpeditionDetailQueryHandler : IRequestHandler<GetAdminExpeditionDetailQuery, AdminExpeditionDetailDto>
{
    private readonly IUnitOfWork _uow;

    public GetAdminExpeditionDetailQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<AdminExpeditionDetailDto> Handle(GetAdminExpeditionDetailQuery request, CancellationToken ct)
    {
        var detail = _uow.Expeditions.Query()
            .Where(x => x.Id == request.Id && x.Type == request.Type)
            .Select(x => new AdminExpeditionDetailDto(
                x.Id,
                x.CategoryId,
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
                x.Status.ToString(),
                x.PublishedAt,
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
                    .ToList()))
            .FirstOrDefault();

        if (detail is null)
        {
            throw new NotFoundException("Expedition not found.");
        }

        return Task.FromResult(detail);
    }
}
