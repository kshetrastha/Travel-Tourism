using Microsoft.AspNetCore.Mvc;
using TravelAndTours.Web.Models;
using TravelAndTours.Web.Services;

namespace TravelAndTours.Web.Controllers;

public class ExpeditionsController : Controller
{
    private const int DefaultPageSize = 9;
    private const int MaxPageSize = 24;
    private readonly IExpeditionsService _expeditions;

    public ExpeditionsController(IExpeditionsService expeditions)
    {
        _expeditions = expeditions;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = DefaultPageSize, CancellationToken ct = default)
    {
        var normalizedPage = Math.Max(1, page);
        var normalizedPageSize = pageSize <= 0 ? DefaultPageSize : Math.Min(pageSize, MaxPageSize);
        var items = await _expeditions.GetPagedAsync(normalizedPage, normalizedPageSize, ct);

        var vm = new ExpeditionsIndexViewModel
        {
            Expeditions = items,
            PageSize = normalizedPageSize
        };

        return View(vm);
    }
}
