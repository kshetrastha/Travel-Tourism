namespace TravelAndTours.Application.Expeditions.Models;

public sealed record AdminExpeditionSummaryDto(
    int Id,
    string Title,
    string Slug,
    string Status,
    string CategoryName,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

public sealed record AdminExpeditionDetailDto(
    int Id,
    int CategoryId,
    string Title,
    string Slug,
    string? ShortTitle,
    string? Tagline,
    int DurationDays,
    int MaxAltitudeMeters,
    string Difficulty,
    string Region,
    string Country,
    string BestSeason,
    int? GroupSizeMin,
    int? GroupSizeMax,
    string? StartingPoint,
    string? EndingPoint,
    string OverviewMarkdown,
    string IncludesMarkdown,
    string ExcludesMarkdown,
    string Status,
    DateTime? PublishedAt,
    IReadOnlyList<ExpeditionFactDto> Facts,
    IReadOnlyList<ExpeditionVariantDto> Variants,
    IReadOnlyList<ItineraryDayDto> ItineraryDays,
    IReadOnlyList<FixedDepartureDto> FixedDepartures,
    IReadOnlyList<MediaAssetDto> Media);
