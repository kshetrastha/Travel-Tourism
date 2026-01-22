namespace TravelAndTours.Web.Models;

public sealed class HomeIndexViewModel
{
    public PagedResult<ExpeditionCardDto> FeaturedExpeditions { get; init; }
        = new([], 1, 3, 0, 0, false, false);
    public int PageSize { get; init; } = 3;
}
