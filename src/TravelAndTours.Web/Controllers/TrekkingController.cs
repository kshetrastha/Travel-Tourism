using Microsoft.AspNetCore.Mvc;
using TravelAndTours.Web.Models;
using TravelAndTours.Web.Services;

namespace TravelAndTours.Web.Controllers;

public class TrekkingController : Controller
{
    private const int DefaultPageSize = 9;
    private const int MaxPageSize = 24;
    private readonly ITrekkingService _trekking;

    public TrekkingController(ITrekkingService trekking)
    {
        _trekking = trekking;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = DefaultPageSize, CancellationToken ct = default)
    {
        var normalizedPage = Math.Max(1, page);
        var normalizedPageSize = pageSize <= 0 ? DefaultPageSize : Math.Min(pageSize, MaxPageSize);
        var items = await _trekking.GetPagedAsync(normalizedPage, normalizedPageSize, ct);

        var vm = new ExpeditionsIndexViewModel
        {
            Expeditions = items,
            PageSize = normalizedPageSize
        };

        return View(vm);
    }
}
