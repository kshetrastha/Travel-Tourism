using Microsoft.AspNetCore.Mvc;
using TravelAndTours.Web.Services;

namespace TravelAndTours.Web.Controllers;

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
}
