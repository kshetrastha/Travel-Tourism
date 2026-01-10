using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Application.Expeditions.Queries.GetExpeditionDetail;
using TravelAndTours.Application.Expeditions.Queries.GetExpeditions;
using TravelAndTours.Application.Expeditions.Queries.GetFixedDepartures;

namespace TravelAndTours.Api.Controllers;

[ApiController]
[Route("api/expeditions")]
[ApiExplorerSettings(GroupName = "public")]
public sealed class ExpeditionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpeditionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PagedResult<ExpeditionCardDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromQuery] string? categorySlug,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 12,
        [FromQuery] string? sortBy = null,
        [FromQuery] string? sortDirection = null,
        CancellationToken ct = default)
    {
        var result = await _mediator.Send(new GetExpeditionsQuery(categorySlug, page, pageSize, sortBy, sortDirection), ct);
        return Ok(result);
    }

    [HttpGet("{slug}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ExpeditionDetailDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBySlug([FromRoute] string slug, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetExpeditionDetailQuery(slug), ct);
        return Ok(result);
    }

    [HttpGet("{slug}/fixed-departures")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IReadOnlyList<FixedDepartureDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFixedDepartures([FromRoute] string slug, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetFixedDeparturesQuery(slug), ct);
        return Ok(result);
    }
}
