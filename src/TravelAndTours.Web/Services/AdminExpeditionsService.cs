using TravelAndTours.Web.Models;

namespace TravelAndTours.Web.Services;

public sealed class AdminExpeditionsService : IAdminExpeditionsService
{
    private readonly ApiClient _api;

    public AdminExpeditionsService(ApiClient api)
    {
        _api = api;
    }

    public async Task<PagedResult<AdminExpeditionListItemDto>> GetAllAsync(int page, int pageSize, CancellationToken ct = default)
        => await _api.GetAsync<PagedResult<AdminExpeditionListItemDto>>($"api/admin/expeditions?page={page}&pageSize={pageSize}", ct)
           ?? new PagedResult<AdminExpeditionListItemDto>([], page, pageSize, 0, 0, false, false);

    public Task<AdminExpeditionDetailDto?> GetByIdAsync(int id, CancellationToken ct = default)
        => _api.GetAsync<AdminExpeditionDetailDto>($"api/admin/expeditions/{id}", ct);

    public async Task<List<ExpeditionCategoryDto>> GetCategoriesAsync(CancellationToken ct = default)
        => await _api.GetAsync<List<ExpeditionCategoryDto>>("api/expedition-categories", ct) ?? [];

    public Task<ApiResponse<int>?> CreateAsync(ExpeditionUpsertViewModel vm, CancellationToken ct = default)
        => _api.PostAsync<ExpeditionUpsertRequest, ApiResponse<int>>(
            "api/admin/expeditions",
            ExpeditionUpsertRequest.From(vm),
            ct);

    public Task<ApiResponse<bool>?> UpdateAsync(int id, ExpeditionUpsertViewModel vm, CancellationToken ct = default)
        => _api.PutAsync<ExpeditionUpsertRequest, ApiResponse<bool>>(
            $"api/admin/expeditions/{id}",
            ExpeditionUpsertRequest.From(vm),
            ct);

    public Task<ApiResponse<bool>?> PublishAsync(int id, CancellationToken ct = default)
        => _api.PostAsync<object, ApiResponse<bool>>($"api/admin/expeditions/{id}/publish", new { }, ct);

    public Task<ApiResponse<bool>?> UnpublishAsync(int id, CancellationToken ct = default)
        => _api.PostAsync<object, ApiResponse<bool>>($"api/admin/expeditions/{id}/unpublish", new { }, ct);

    public Task<ApiResponse<bool>?> ReplaceItineraryAsync(int id, IReadOnlyList<ItineraryDayInputViewModel> days, CancellationToken ct = default)
        => _api.PostAsync<ReplaceItineraryRequest, ApiResponse<bool>>(
            $"api/admin/expeditions/{id}/itinerary",
            new ReplaceItineraryRequest(days),
            ct);

    public Task<ApiResponse<bool>?> ReplaceFixedDeparturesAsync(int id, IReadOnlyList<FixedDepartureInputViewModel> departures, CancellationToken ct = default)
        => _api.PostAsync<ReplaceFixedDeparturesRequest, ApiResponse<bool>>(
            $"api/admin/expeditions/{id}/fixed-departures",
            new ReplaceFixedDeparturesRequest(departures),
            ct);

    public Task<ApiResponse<bool>?> ReplaceMediaAsync(int id, IReadOnlyList<MediaAssetInputViewModel> media, CancellationToken ct = default)
        => _api.PostAsync<ReplaceMediaRequest, ApiResponse<bool>>(
            $"api/admin/expeditions/{id}/media",
            new ReplaceMediaRequest(media),
            ct);

    private sealed record ExpeditionFactInput(string Label, string Value, int SortOrder);
    private sealed record ExpeditionVariantInput(
        string VariantType,
        string? TitleOverride,
        decimal PriceFrom,
        bool IsActive,
        string? InclusionsOverrideMarkdown,
        string? ExclusionsOverrideMarkdown);
    private sealed record ExpeditionUpsertRequest(
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
        IReadOnlyList<ExpeditionVariantInput> Variants)
    {
        public static ExpeditionUpsertRequest From(ExpeditionUpsertViewModel vm)
            => new(
                vm.CategoryId,
                vm.Title,
                vm.Slug,
                vm.ShortTitle,
                vm.Tagline,
                vm.DurationDays,
                vm.MaxAltitudeMeters,
                vm.Difficulty,
                vm.Region,
                vm.Country,
                vm.BestSeason,
                vm.GroupSizeMin,
                vm.GroupSizeMax,
                vm.StartingPoint,
                vm.EndingPoint,
                vm.OverviewMarkdown,
                vm.IncludesMarkdown,
                vm.ExcludesMarkdown,
                vm.Facts.Select(f => new ExpeditionFactInput(f.Label, f.Value, f.SortOrder)).ToList(),
                vm.Variants.Select(v => new ExpeditionVariantInput(
                    v.VariantType,
                    v.TitleOverride,
                    v.PriceFrom,
                    v.IsActive,
                    v.InclusionsOverrideMarkdown,
                    v.ExclusionsOverrideMarkdown)).ToList());
    }

    private sealed record ItineraryDayInput(
        int DayNumber,
        string Title,
        string DescriptionMarkdown,
        string? Meals,
        string? Accommodation,
        int? ElevationMeters);

    private sealed record FixedDepartureInput(
        DateTime StartDate,
        DateTime EndDate,
        decimal Price,
        string Currency,
        int SlotsTotal,
        int SlotsAvailable,
        string Status,
        string? Notes,
        int? VariantId);

    private sealed record MediaAssetInput(
        string MediaType,
        string Url,
        string? ThumbnailUrl,
        string? Title,
        int SortOrder);

    private sealed record ReplaceItineraryRequest(IReadOnlyList<ItineraryDayInput> Days)
    {
        public ReplaceItineraryRequest(IReadOnlyList<ItineraryDayInputViewModel> days)
            : this(days.Select(d => new ItineraryDayInput(
                d.DayNumber,
                d.Title,
                d.DescriptionMarkdown,
                d.Meals,
                d.Accommodation,
                d.ElevationMeters)).ToList())
        {
        }
    }

    private sealed record ReplaceFixedDeparturesRequest(IReadOnlyList<FixedDepartureInput> Departures)
    {
        public ReplaceFixedDeparturesRequest(IReadOnlyList<FixedDepartureInputViewModel> departures)
            : this(departures.Select(d => new FixedDepartureInput(
                d.StartDate,
                d.EndDate,
                d.Price,
                d.Currency,
                d.SlotsTotal,
                d.SlotsAvailable,
                d.Status,
                d.Notes,
                d.VariantId)).ToList())
        {
        }
    }

    private sealed record ReplaceMediaRequest(IReadOnlyList<MediaAssetInput> Media)
    {
        public ReplaceMediaRequest(IReadOnlyList<MediaAssetInputViewModel> media)
            : this(media.Select(m => new MediaAssetInput(
                m.MediaType,
                m.Url,
                m.ThumbnailUrl,
                m.Title,
                m.SortOrder)).ToList())
        {
        }
    }
}
