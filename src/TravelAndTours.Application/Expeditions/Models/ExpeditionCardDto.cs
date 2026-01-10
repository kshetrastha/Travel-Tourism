namespace TravelAndTours.Application.Expeditions.Models;

public sealed record ExpeditionCardDto(
    int Id,
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
    string CategorySlug,
    string? CoverImageUrl,
    decimal? PriceFrom,
    DateTime? PublishedAt);
