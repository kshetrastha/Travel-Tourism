using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Expeditions.Commands.CreateExpeditionCategory;
using TravelAndTours.Application.Expeditions.Commands.DeleteExpeditionCategory;
using TravelAndTours.Application.Expeditions.Commands.UpdateExpeditionCategory;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Application.Expeditions.Queries.GetAdminExpeditionCategories;
using TravelAndTours.Application.Expeditions.Queries.GetAdminExpeditionCategoryById;

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

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ExpeditionCategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAdminExpeditionCategoriesQuery(), ct);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ExpeditionCategoryDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAdminExpeditionCategoryByIdQuery(id), ct);
        return Ok(result);
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

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new DeleteExpeditionCategoryCommand(id), ct);
        return Ok(result);
    }
}
