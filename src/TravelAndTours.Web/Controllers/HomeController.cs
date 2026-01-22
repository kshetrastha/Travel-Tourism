using Microsoft.AspNetCore.Mvc;
using TravelAndTours.Web.Models;
using TravelAndTours.Web.Services;

namespace TravelAndTours.Web.Controllers;

public class HomeController : Controller
{
    private readonly IExpeditionsService _expeditionsService;

    public HomeController(IExpeditionsService expeditionsService)
    {
        _expeditionsService = expeditionsService;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        const int pageSize = 3;
        var expeditions = await _expeditionsService.GetPagedAsync(1, pageSize, ct);
        var model = new HomeIndexViewModel
        {
            FeaturedExpeditions = expeditions,
            PageSize = pageSize
        };

        return View(model);
    }

    public IActionResult Error() => View();
}
