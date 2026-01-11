using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Expeditions.Commands.CreateExpedition;
using TravelAndTours.Application.Expeditions.Commands.PublishExpedition;
using TravelAndTours.Application.Expeditions.Commands.ReplaceFixedDepartures;
using TravelAndTours.Application.Expeditions.Commands.ReplaceItinerary;
using TravelAndTours.Application.Expeditions.Commands.ReplaceMedia;
using TravelAndTours.Application.Expeditions.Commands.UnpublishExpedition;
using TravelAndTours.Application.Expeditions.Commands.UpdateExpedition;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Application.Expeditions.Queries.GetAdminExpeditionDetail;
using TravelAndTours.Application.Expeditions.Queries.GetAdminExpeditions;

namespace TravelAndTours.Api.Controllers;

[ApiController]
[Route("api/admin/expeditions")]
[Authorize(Roles = "ADMIN")]
[ApiExplorerSettings(GroupName = "admin")]
public sealed class AdminExpeditionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminExpeditionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<AdminExpeditionSummaryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAdminExpeditionsQuery(), ct);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AdminExpeditionDetailDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAdminExpeditionDetailQuery(id), ct);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] ExpeditionUpsertModel request, CancellationToken ct)
    {
        var result = await _mediator.Send(new CreateExpeditionCommand(request), ct);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ExpeditionUpsertModel request, CancellationToken ct)
    {
        var result = await _mediator.Send(new UpdateExpeditionCommand(id, request), ct);
        return Ok(result);
    }

    [HttpPost("{id:int}/publish")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Publish([FromRoute] int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new PublishExpeditionCommand(id), ct);
        return Ok(result);
    }

    [HttpPost("{id:int}/unpublish")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Unpublish([FromRoute] int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new UnpublishExpeditionCommand(id), ct);
        return Ok(result);
    }

    [HttpPost("{id:int}/itinerary")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ReplaceItinerary([FromRoute] int id, [FromBody] ReplaceItineraryRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new ReplaceItineraryCommand(id, request.Days), ct);
        return Ok(result);
    }

    [HttpPost("{id:int}/fixed-departures")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ReplaceFixedDepartures([FromRoute] int id, [FromBody] ReplaceFixedDeparturesRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new ReplaceFixedDeparturesCommand(id, request.Departures), ct);
        return Ok(result);
    }

    [HttpPost("{id:int}/media")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ReplaceMedia([FromRoute] int id, [FromBody] ReplaceMediaRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new ReplaceMediaCommand(id, request.Media), ct);
        return Ok(result);
    }
}
