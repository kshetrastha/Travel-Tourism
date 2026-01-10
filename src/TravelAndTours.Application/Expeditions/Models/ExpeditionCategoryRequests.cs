namespace TravelAndTours.Application.Expeditions.Models;

public sealed record CreateExpeditionCategoryRequest(
    string Name,
    string Slug,
    int? ParentCategoryId,
    int SortOrder,
    bool IsActive);

public sealed record UpdateExpeditionCategoryRequest(
    string Name,
    string Slug,
    int? ParentCategoryId,
    int SortOrder,
    bool IsActive);
