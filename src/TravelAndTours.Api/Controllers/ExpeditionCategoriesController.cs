using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Application.Expeditions.Queries.GetExpeditionCategories;

namespace TravelAndTours.Api.Controllers;

[ApiController]
[Route("api/expedition-categories")]
[ApiExplorerSettings(GroupName = "public")]
public sealed class ExpeditionCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpeditionCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
    [ProducesResponseType(typeof(IReadOnlyList<ExpeditionCategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetExpeditionCategoriesQuery(), ct);
        return Ok(result);
    }
}
