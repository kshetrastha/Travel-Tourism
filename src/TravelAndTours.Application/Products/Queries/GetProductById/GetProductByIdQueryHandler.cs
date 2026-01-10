using MediatR;
using TravelAndTours.Application.Common.Errors;
using TravelAndTours.Application.Common.Mapping;
using TravelAndTours.Application.Products.Models;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Products.Queries.GetProductById;

public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IUnitOfWork _uow;

    public GetProductByIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken ct)
    {
        var entity = await _uow.Products.GetByIdAsync(request.Id, ct);
        if (entity is null)
            throw new NotFoundException($"Product {request.Id} not found.");

        return entity.ToDto();
    }
}
