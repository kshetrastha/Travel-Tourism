using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAndTours.Application.Products.Commands.CreateProduct;
using TravelAndTours.Application.Products.Commands.DeleteProduct;
using TravelAndTours.Application.Products.Commands.UpdateProduct;
using TravelAndTours.Application.Products.Models;
using TravelAndTours.Application.Products.Queries.GetProductById;
using TravelAndTours.Application.Products.Queries.GetProducts;

namespace TravelAndTours.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IReadOnlyList<ProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 50, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new GetProductsQuery(page, pageSize), ct);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id), ct);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken ct)
    {
        var id = await _mediator.Send(new CreateProductCommand(request.Name, request.Price), ct);
        return Ok(id);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new UpdateProductCommand(id, request.Name, request.Price), ct);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id), ct);
        return Ok(result);
    }
}
