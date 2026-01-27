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

    [HttpGet("expeditions/{slug}")]
    public async Task<IActionResult> Details(string slug, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            return NotFound();
        }

        var detail = await _expeditions.GetDetailAsync(slug, ct);
        if (detail is null)
        {
            return NotFound();
        }

        var heroMedia = detail.Media.FirstOrDefault();
        var vm = new ExpeditionDetailViewModel
        {
            Expedition = detail,
            HeroMedia = heroMedia
        };

        return View(vm);
    }
}
