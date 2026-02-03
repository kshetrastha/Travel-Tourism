using Microsoft.AspNetCore.Mvc;
using TravelAndTours.Web.Filters;
using TravelAndTours.Web.Models;
using TravelAndTours.Web.Services;

namespace TravelAndTours.Web.Areas.Admin.Controllers;

[Area("Admin")]
[RequireRole("Admin")]
public class ExpeditionCategoriesController : Controller
{
    private readonly IAdminExpeditionCategoriesService _categories;

    public ExpeditionCategoriesController(IAdminExpeditionCategoriesService categories)
    {
        _categories = categories;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var items = await _categories.GetAllAsync(ct);
        return View(items);
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken ct)
    {
        var parents = await _categories.GetAllAsync(ct);
        var vm = new ExpeditionCategoryEditViewModel
        {
            Category = new ExpeditionCategoryUpsertViewModel
            {
                IsActive = true,
                SortOrder = 0
            },
            ParentOptions = parents
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind(Prefix = "Category")] ExpeditionCategoryUpsertViewModel category, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            var parents = await _categories.GetAllAsync(ct);
            return View(new ExpeditionCategoryEditViewModel
            {
                Category = category,
                ParentOptions = parents
            });
        }

        var response = await _categories.CreateAsync(category, ct);
        if (response?.Success != true)
        {
            ModelState.AddModelError(string.Empty, response?.Message ?? "Unable to create category.");
            var parents = await _categories.GetAllAsync(ct);
            return View(new ExpeditionCategoryEditViewModel
            {
                Category = category,
                ParentOptions = parents
            });
        }

        TempData["StatusMessage"] = response.Message;
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var item = await _categories.GetByIdAsync(id, ct);
        if (item is null) return NotFound();

        var parents = (await _categories.GetAllAsync(ct))
            .Where(x => x.Id != id)
            .ToList();

        var vm = new ExpeditionCategoryEditViewModel
        {
            Category = new ExpeditionCategoryUpsertViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Slug = item.Slug,
                ParentCategoryId = item.ParentCategoryId,
                SortOrder = item.SortOrder,
                IsActive = item.IsActive
            },
            ParentOptions = parents
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind(Prefix = "Category")] ExpeditionCategoryUpsertViewModel category, CancellationToken ct)
    {
        category.Id = id;
        if (!ModelState.IsValid)
        {
            var parents = (await _categories.GetAllAsync(ct))
                .Where(x => x.Id != id)
                .ToList();
            return View(new ExpeditionCategoryEditViewModel
            {
                Category = category,
                ParentOptions = parents
            });
        }

        var response = await _categories.UpdateAsync(id, category, ct);
        if (response?.Success != true)
        {
            ModelState.AddModelError(string.Empty, response?.Message ?? "Unable to update category.");
            var parents = (await _categories.GetAllAsync(ct))
                .Where(x => x.Id != id)
                .ToList();
            return View(new ExpeditionCategoryEditViewModel
            {
                Category = category,
                ParentOptions = parents
            });
        }

        TempData["StatusMessage"] = response.Message;
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var response = await _categories.DeleteAsync(id, ct);
        TempData["StatusMessage"] = response?.Message ?? "Unable to delete category.";
        return RedirectToAction(nameof(Index));
    }
}
