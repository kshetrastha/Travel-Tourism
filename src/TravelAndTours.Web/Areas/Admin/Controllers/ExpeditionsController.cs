using Microsoft.AspNetCore.Mvc;
using TravelAndTours.Web.Filters;
using TravelAndTours.Web.Models;
using TravelAndTours.Web.Services;

namespace TravelAndTours.Web.Areas.Admin.Controllers;

[Area("Admin")]
[RequireRole("Admin")]
public class ExpeditionsController : Controller
{
    private readonly IAdminExpeditionsService _expeditions;

    public ExpeditionsController(IAdminExpeditionsService expeditions)
    {
        _expeditions = expeditions;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
    {
        var currentPage = page < 1 ? 1 : page;
        var currentPageSize = pageSize < 1 ? 10 : pageSize;
        var items = await _expeditions.GetAllAsync(currentPage, currentPageSize, ct);
        return View(new AdminExpeditionsIndexViewModel
        {
            Expeditions = items,
            PageSize = currentPageSize
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Table([FromForm] int page = 1, [FromForm] int pageSize = 10, CancellationToken ct = default)
    {
        var currentPage = page < 1 ? 1 : page;
        var currentPageSize = pageSize < 1 ? 10 : pageSize;
        var items = await _expeditions.GetAllAsync(currentPage, currentPageSize, ct);
        var vm = new AdminExpeditionsIndexViewModel
        {
            Expeditions = items,
            PageSize = currentPageSize
        };
        return PartialView("_ExpeditionsTable", vm);
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken ct)
    {
        var categories = await _expeditions.GetCategoriesAsync(ct);
        var vm = new ExpeditionCreateViewModel
        {
            Categories = categories,
            Expedition = BuildDefaultExpeditionModel()
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ExpeditionUpsertViewModel expedition, CancellationToken ct)
    {
        expedition.Facts = NormalizeFacts(expedition.Facts);
        expedition.Variants = NormalizeVariants(expedition.Variants);

        ValidateBaseModel(expedition);

        if (!ModelState.IsValid)
        {
            var categories = await _expeditions.GetCategoriesAsync(ct);
            return View(new ExpeditionCreateViewModel
            {
                Categories = categories,
                Expedition = EnsureLists(expedition)
            });
        }

        var response = await _expeditions.CreateAsync(expedition, ct);
        if (response?.Success != true || response.Data >0)
        {
            ModelState.AddModelError(string.Empty, response?.Message ?? "Unable to create expedition.");
            var categories = await _expeditions.GetCategoriesAsync(ct);
            return View(new ExpeditionCreateViewModel
            {
                Categories = categories,
                Expedition = EnsureLists(expedition)
            });
        }

        TempData["StatusMessage"] = response.Message;
        return RedirectToAction(nameof(Edit), new { id = response.Data });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var detail = await _expeditions.GetByIdAsync(id, ct);
        if (detail is null) return NotFound();

        var categories = await _expeditions.GetCategoriesAsync(ct);
        var vm = BuildEditViewModel(detail, categories);
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateBase(int id, ExpeditionUpsertViewModel expedition, CancellationToken ct)
    {
        expedition.Id = id;
        expedition.Facts = NormalizeFacts(expedition.Facts);
        expedition.Variants = NormalizeVariants(expedition.Variants);

        ValidateBaseModel(expedition);

        if (!ModelState.IsValid)
        {
            return await RenderEditWithBaseAsync(id, expedition, ct);
        }

        var response = await _expeditions.UpdateAsync(id, expedition, ct);
        if (response?.Success != true)
        {
            ModelState.AddModelError(string.Empty, response?.Message ?? "Unable to update expedition.");
            return await RenderEditWithBaseAsync(id, expedition, ct);
        }

        TempData["StatusMessage"] = response.Message;
        return RedirectToAction(nameof(Edit), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateItinerary(int id, [Bind(Prefix = "ItineraryDays")] List<ItineraryDayInputViewModel> itineraryDays, CancellationToken ct)
    {
        var sanitized = NormalizeItinerary(itineraryDays);
        var response = await _expeditions.ReplaceItineraryAsync(id, sanitized, ct);
        if (response?.Success != true)
        {
            TempData["StatusMessage"] = response?.Message ?? "Unable to update itinerary.";
        }
        else
        {
            TempData["StatusMessage"] = response.Message;
        }

        return RedirectToAction(nameof(Edit), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateFixedDepartures(int id, [Bind(Prefix = "FixedDepartures")] List<FixedDepartureInputViewModel> fixedDepartures, CancellationToken ct)
    {
        var sanitized = NormalizeFixedDepartures(fixedDepartures);
        var response = await _expeditions.ReplaceFixedDeparturesAsync(id, sanitized, ct);
        if (response?.Success != true)
        {
            TempData["StatusMessage"] = response?.Message ?? "Unable to update fixed departures.";
        }
        else
        {
            TempData["StatusMessage"] = response.Message;
        }

        return RedirectToAction(nameof(Edit), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateMedia(int id, [Bind(Prefix = "Media")] List<MediaAssetInputViewModel> media, CancellationToken ct)
    {
        var sanitized = NormalizeMedia(media);
        var response = await _expeditions.ReplaceMediaAsync(id, sanitized, ct);
        if (response?.Success != true)
        {
            TempData["StatusMessage"] = response?.Message ?? "Unable to update media.";
        }
        else
        {
            TempData["StatusMessage"] = response.Message;
        }

        return RedirectToAction(nameof(Edit), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Publish(int id, CancellationToken ct)
    {
        var response = await _expeditions.PublishAsync(id, ct);
        TempData["StatusMessage"] = response?.Message ?? "Unable to publish expedition.";
        return RedirectToAction(nameof(Edit), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Unpublish(int id, CancellationToken ct)
    {
        var response = await _expeditions.UnpublishAsync(id, ct);
        TempData["StatusMessage"] = response?.Message ?? "Unable to unpublish expedition.";
        return RedirectToAction(nameof(Edit), new { id });
    }

    private async Task<IActionResult> RenderEditWithBaseAsync(int id, ExpeditionUpsertViewModel expedition, CancellationToken ct)
    {
        var detail = await _expeditions.GetByIdAsync(id, ct);
        if (detail is null) return NotFound();

        var categories = await _expeditions.GetCategoriesAsync(ct);
        var vm = BuildEditViewModel(detail, categories);
        vm.Expedition = EnsureLists(expedition);
        return View("Edit", vm);
    }

    private static ExpeditionUpsertViewModel BuildDefaultExpeditionModel()
        => EnsureLists(new ExpeditionUpsertViewModel
        {
            DurationDays = 1,
            MaxAltitudeMeters = 1,
            Difficulty = "Moderate",
            GroupSizeMin = 4,
            GroupSizeMax = 12
        });

    private static ExpeditionAdminEditViewModel BuildEditViewModel(AdminExpeditionDetailDto detail, List<ExpeditionCategoryDto> categories)
        => new()
        {
            Id = detail.Id,
            Status = detail.Status,
            PublishedAt = detail.PublishedAt,
            Categories = categories,
            Expedition = EnsureLists(new ExpeditionUpsertViewModel
            {
                Id = detail.Id,
                CategoryId = detail.CategoryId,
                Title = detail.Title,
                Slug = detail.Slug,
                ShortTitle = detail.ShortTitle,
                Tagline = detail.Tagline,
                DurationDays = detail.DurationDays,
                MaxAltitudeMeters = detail.MaxAltitudeMeters,
                Difficulty = detail.Difficulty,
                Region = detail.Region,
                Country = detail.Country,
                BestSeason = detail.BestSeason,
                GroupSizeMin = detail.GroupSizeMin,
                GroupSizeMax = detail.GroupSizeMax,
                StartingPoint = detail.StartingPoint,
                EndingPoint = detail.EndingPoint,
                OverviewMarkdown = detail.OverviewMarkdown,
                IncludesMarkdown = detail.IncludesMarkdown,
                ExcludesMarkdown = detail.ExcludesMarkdown,
                Facts = detail.Facts.Select(f => new ExpeditionFactInputViewModel
                {
                    Label = f.Label,
                    Value = f.Value,
                    SortOrder = f.SortOrder
                }).ToList(),
                Variants = detail.Variants.Select(v => new ExpeditionVariantInputViewModel
                {
                    VariantType = v.VariantType,
                    TitleOverride = v.TitleOverride,
                    PriceFrom = v.PriceFrom,
                    IsActive = v.IsActive,
                    InclusionsOverrideMarkdown = v.InclusionsOverrideMarkdown,
                    ExclusionsOverrideMarkdown = v.ExclusionsOverrideMarkdown
                }).ToList()
            }),
            ItineraryDays = EnsureList(detail.ItineraryDays.Select(d => new ItineraryDayInputViewModel
            {
                DayNumber = d.DayNumber,
                Title = d.Title,
                DescriptionMarkdown = d.DescriptionMarkdown,
                Meals = d.Meals,
                Accommodation = d.Accommodation,
                ElevationMeters = d.ElevationMeters
            }).ToList(), () => new ItineraryDayInputViewModel()),
            FixedDepartures = EnsureList(detail.FixedDepartures.Select(d => new FixedDepartureInputViewModel
            {
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                Price = d.Price,
                Currency = d.Currency,
                SlotsTotal = d.SlotsTotal,
                SlotsAvailable = d.SlotsAvailable,
                Status = d.Status,
                Notes = d.Notes,
                VariantId = d.VariantId
            }).ToList(), () => new FixedDepartureInputViewModel
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1)
            }),
            Media = EnsureList(detail.Media.Select(m => new MediaAssetInputViewModel
            {
                MediaType = m.MediaType,
                Url = m.Url,
                ThumbnailUrl = m.ThumbnailUrl,
                Title = m.Title,
                SortOrder = m.SortOrder
            }).ToList(), () => new MediaAssetInputViewModel())
        };

    private static ExpeditionUpsertViewModel EnsureLists(ExpeditionUpsertViewModel model)
    {
        model.Facts = EnsureList(model.Facts, () => new ExpeditionFactInputViewModel { SortOrder = 1 });
        model.Variants = EnsureList(model.Variants, () => new ExpeditionVariantInputViewModel
        {
            VariantType = "Standard",
            PriceFrom = 1,
            IsActive = true
        });
        return model;
    }

    private static List<T> EnsureList<T>(List<T> items, Func<T> factory)
    {
        if (items.Count == 0)
        {
            items.Add(factory());
        }

        return items;
    }

    private void ValidateBaseModel(ExpeditionUpsertViewModel model)
    {
        ModelState.Clear();
        TryValidateModel(model);

        if (model.Facts.Count == 0)
        {
            ModelState.AddModelError(nameof(model.Facts), "Add at least one fact.");
        }

        if (model.Variants.Count == 0)
        {
            ModelState.AddModelError(nameof(model.Variants), "Add at least one variant.");
        }
    }

    private static List<ExpeditionFactInputViewModel> NormalizeFacts(List<ExpeditionFactInputViewModel> facts)
        => facts
            .Where(f => !(string.IsNullOrWhiteSpace(f.Label) && string.IsNullOrWhiteSpace(f.Value)))
            .ToList();

    private static List<ExpeditionVariantInputViewModel> NormalizeVariants(List<ExpeditionVariantInputViewModel> variants)
        => variants
            .Where(v => !(string.IsNullOrWhiteSpace(v.VariantType)
                          && v.PriceFrom <= 0
                          && string.IsNullOrWhiteSpace(v.TitleOverride)
                          && string.IsNullOrWhiteSpace(v.InclusionsOverrideMarkdown)
                          && string.IsNullOrWhiteSpace(v.ExclusionsOverrideMarkdown)))
            .ToList();

    private static List<ItineraryDayInputViewModel> NormalizeItinerary(List<ItineraryDayInputViewModel> days)
        => days
            .Where(d => !(d.DayNumber == 0
                          && string.IsNullOrWhiteSpace(d.Title)
                          && string.IsNullOrWhiteSpace(d.DescriptionMarkdown)
                          && string.IsNullOrWhiteSpace(d.Meals)
                          && string.IsNullOrWhiteSpace(d.Accommodation)
                          && d.ElevationMeters is null))
            .ToList();

    private static List<FixedDepartureInputViewModel> NormalizeFixedDepartures(List<FixedDepartureInputViewModel> departures)
        => departures
            .Where(d => !(d.StartDate == default
                          && d.EndDate == default
                          && d.Price == 0
                          && string.IsNullOrWhiteSpace(d.Currency)
                          && d.SlotsTotal == 0
                          && d.SlotsAvailable == 0
                          && string.IsNullOrWhiteSpace(d.Status)
                          && string.IsNullOrWhiteSpace(d.Notes)
                          && d.VariantId is null))
            .ToList();

    private static List<MediaAssetInputViewModel> NormalizeMedia(List<MediaAssetInputViewModel> media)
        => media
            .Where(m => !(string.IsNullOrWhiteSpace(m.Url)
                          && string.IsNullOrWhiteSpace(m.Title)
                          && string.IsNullOrWhiteSpace(m.ThumbnailUrl)
                          && string.IsNullOrWhiteSpace(m.MediaType)))
            .ToList();
}
