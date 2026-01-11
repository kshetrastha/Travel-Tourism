using Microsoft.AspNetCore.Mvc;
using TravelAndTours.Web.Filters;
using TravelAndTours.Web.Models;
using TravelAndTours.Web.Services;

namespace TravelAndTours.Web.Areas.Admin.Controllers;

[Area("Admin")]
[RequireRole("Admin")]
public class ProductsController : Controller
{
    private readonly IProductsService _products;

    public ProductsController(IProductsService products)
    {
        _products = products;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var items = await _products.GetAllAsync(ct);
        return View(items);
    }

    [HttpGet]
    public IActionResult Create() => View(new ProductUpsertViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductUpsertViewModel vm, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(vm);
        await _products.CreateAsync(vm, ct);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var item = await _products.GetByIdAsync(id, ct);
        if (item is null) return NotFound();

        return View(new ProductUpsertViewModel
        {
            Id = item.Id,
            Name = item.Name,
            Price = item.Price
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProductUpsertViewModel vm, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(vm);
        await _products.UpdateAsync(vm, ct);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _products.DeleteAsync(id, ct);
        return RedirectToAction(nameof(Index));
    }
}
