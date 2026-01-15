using System.ComponentModel.DataAnnotations;

namespace TravelAndTours.Web.Models;

public sealed class ApiResponse<T>
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public T? Data { get; init; }
    public List<string> Errors { get; init; } = [];
}

public sealed record PagedResult<T>(
    IReadOnlyList<T> Items,
    int Page,
    int PageSize,
    long TotalCount,
    int TotalPages,
    bool HasNextPage,
    bool HasPreviousPage);

public sealed record ExpeditionCategoryDto(
    int Id,
    string Name,
    string Slug,
    int? ParentCategoryId,
    int SortOrder,
    bool IsActive);

public sealed record AdminExpeditionListItemDto(
    int Id,
    string Title,
    string Slug,
    string Status,
    string CategoryName,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

public sealed class AdminExpeditionsIndexViewModel
{
    public PagedResult<AdminExpeditionListItemDto> Expeditions { get; init; }
        = new([], 1, 10, 0, 0, false, false);
    public int PageSize { get; init; } = 10;
}

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

public sealed record ExpeditionFactDto(
    int Id,
    string Label,
    string Value,
    int SortOrder);

public sealed record ExpeditionVariantDto(
    int Id,
    string VariantType,
    string? TitleOverride,
    decimal PriceFrom,
    bool IsActive,
    string? InclusionsOverrideMarkdown,
    string? ExclusionsOverrideMarkdown);

public sealed record ItineraryDayDto(
    int Id,
    int DayNumber,
    string Title,
    string DescriptionMarkdown,
    string? Meals,
    string? Accommodation,
    int? ElevationMeters);

public sealed record FixedDepartureDto(
    int Id,
    DateTime StartDate,
    DateTime EndDate,
    decimal Price,
    string Currency,
    int SlotsTotal,
    int SlotsAvailable,
    string Status,
    string? Notes,
    int? VariantId);

public sealed record MediaAssetDto(
    int Id,
    string MediaType,
    string Url,
    string? ThumbnailUrl,
    string? Title,
    int SortOrder);

public sealed class ExpeditionFactInputViewModel
{
    [Required]
    [StringLength(120)]
    public string Label { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Value { get; set; } = string.Empty;

    [Range(0, 999)]
    public int SortOrder { get; set; }
}

public sealed class ExpeditionVariantInputViewModel
{
    [Required]
    public string VariantType { get; set; } = string.Empty;

    [StringLength(120)]
    public string? TitleOverride { get; set; }

    [Range(0.01, 999999)]
    public decimal PriceFrom { get; set; }

    public bool IsActive { get; set; }

    public string? InclusionsOverrideMarkdown { get; set; }

    public string? ExclusionsOverrideMarkdown { get; set; }
}

public sealed class ItineraryDayInputViewModel
{
    [Range(1, 365)]
    public int DayNumber { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string DescriptionMarkdown { get; set; } = string.Empty;

    public string? Meals { get; set; }

    public string? Accommodation { get; set; }

    public int? ElevationMeters { get; set; }
}

public sealed class FixedDepartureInputViewModel
{
    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Range(0.01, 999999)]
    public decimal Price { get; set; }

    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string Currency { get; set; } = "USD";

    [Range(1, 999)]
    public int SlotsTotal { get; set; }

    [Range(0, 999)]
    public int SlotsAvailable { get; set; }

    [Required]
    public string Status { get; set; } = "Open";

    public string? Notes { get; set; }

    public int? VariantId { get; set; }
}

public sealed class MediaAssetInputViewModel
{
    [Required]
    public string MediaType { get; set; } = "Image";

    [Required]
    [StringLength(500)]
    public string Url { get; set; } = string.Empty;

    public string? ThumbnailUrl { get; set; }

    public string? Title { get; set; }

    [Range(0, 999)]
    public int SortOrder { get; set; }
}

public sealed class ExpeditionUpsertViewModel
{
    public int? Id { get; set; }

    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }

    [Required]
    [StringLength(250)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Slug { get; set; } = string.Empty;

    [StringLength(120)]
    public string? ShortTitle { get; set; }

    [StringLength(200)]
    public string? Tagline { get; set; }

    [Range(1, 365)]
    public int DurationDays { get; set; }

    [Range(1, 20000)]
    public int MaxAltitudeMeters { get; set; }

    [Required]
    public string Difficulty { get; set; } = string.Empty;

    [Required]
    [StringLength(120)]
    public string Region { get; set; } = string.Empty;

    [Required]
    [StringLength(120)]
    public string Country { get; set; } = string.Empty;

    [Required]
    [StringLength(120)]
    public string BestSeason { get; set; } = string.Empty;

    public int? GroupSizeMin { get; set; }

    public int? GroupSizeMax { get; set; }

    [StringLength(120)]
    public string? StartingPoint { get; set; }

    [StringLength(120)]
    public string? EndingPoint { get; set; }

    [Required]
    public string OverviewMarkdown { get; set; } = string.Empty;

    [Required]
    public string IncludesMarkdown { get; set; } = string.Empty;

    [Required]
    public string ExcludesMarkdown { get; set; } = string.Empty;

    public List<ExpeditionFactInputViewModel> Facts { get; set; } = [];

    public List<ExpeditionVariantInputViewModel> Variants { get; set; } = [];
}

public sealed class ExpeditionCreateViewModel
{
    public ExpeditionUpsertViewModel Expedition { get; set; } = new();
    public List<ExpeditionCategoryDto> Categories { get; set; } = [];
}

public sealed class ExpeditionAdminEditViewModel
{
    public int Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? PublishedAt { get; set; }
    public ExpeditionUpsertViewModel Expedition { get; set; } = new();
    public List<ExpeditionCategoryDto> Categories { get; set; } = [];
    public List<ItineraryDayInputViewModel> ItineraryDays { get; set; } = [];
    public List<FixedDepartureInputViewModel> FixedDepartures { get; set; } = [];
    public List<MediaAssetInputViewModel> Media { get; set; } = [];
}
