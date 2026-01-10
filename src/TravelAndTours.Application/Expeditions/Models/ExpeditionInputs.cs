namespace TravelAndTours.Application.Expeditions.Models;

public sealed record ExpeditionVariantInput(
    string VariantType,
    string? TitleOverride,
    decimal PriceFrom,
    bool IsActive,
    string? InclusionsOverrideMarkdown,
    string? ExclusionsOverrideMarkdown);

public sealed record ExpeditionFactInput(
    string Label,
    string Value,
    int SortOrder);

public sealed record ItineraryDayInput(
    int DayNumber,
    string Title,
    string DescriptionMarkdown,
    string? Meals,
    string? Accommodation,
    int? ElevationMeters);

public sealed record FixedDepartureInput(
    DateTime StartDate,
    DateTime EndDate,
    decimal Price,
    string Currency,
    int SlotsTotal,
    int SlotsAvailable,
    string Status,
    string? Notes,
    int? VariantId);

public sealed record MediaAssetInput(
    string MediaType,
    string Url,
    string? ThumbnailUrl,
    string? Title,
    int SortOrder);

public sealed record ExpeditionUpsertModel(
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
    IReadOnlyList<ExpeditionFactInput> Facts,
    IReadOnlyList<ExpeditionVariantInput> Variants);
