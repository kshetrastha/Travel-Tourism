namespace TravelAndTours.Application.Expeditions.Models;

public sealed record MediaAssetDto(
    int Id,
    string MediaType,
    string Url,
    string? ThumbnailUrl,
    string? Title,
    int SortOrder);
