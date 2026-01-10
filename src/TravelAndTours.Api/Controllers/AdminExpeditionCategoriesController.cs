using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Expeditions.Commands.CreateExpeditionCategory;
using TravelAndTours.Application.Expeditions.Commands.UpdateExpeditionCategory;
using TravelAndTours.Application.Expeditions.Models;

namespace TravelAndTours.Api.Controllers;

[ApiController]
[Route("api/admin/expedition-categories")]
[Authorize(Roles = "ADMIN")]
[ApiExplorerSettings(GroupName = "admin")]
public sealed class AdminExpeditionCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminExpeditionCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateExpeditionCategoryRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new CreateExpeditionCategoryCommand(
            request.Name,
            request.Slug,
            request.ParentCategoryId,
            request.SortOrder,
            request.IsActive), ct);

        return Ok(result);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateExpeditionCategoryRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new UpdateExpeditionCategoryCommand(
            id,
            request.Name,
            request.Slug,
            request.ParentCategoryId,
            request.SortOrder,
            request.IsActive), ct);

        return Ok(result);
    }
}
