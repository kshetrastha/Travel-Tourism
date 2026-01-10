namespace TravelAndTours.Application.Expeditions.Models;

public sealed record ExpeditionVariantDto(
    int Id,
    string VariantType,
    string? TitleOverride,
    decimal PriceFrom,
    bool IsActive,
    string? InclusionsOverrideMarkdown,
    string? ExclusionsOverrideMarkdown);
