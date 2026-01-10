namespace TravelAndTours.Application.Expeditions.Models;

public sealed record ExpeditionCategoryDto(
    int Id,
    string Name,
    string Slug,
    int? ParentCategoryId,
    int SortOrder,
    bool IsActive);
