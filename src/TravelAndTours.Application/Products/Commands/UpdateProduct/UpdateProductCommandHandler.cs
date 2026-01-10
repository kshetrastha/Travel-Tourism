using MediatR;
using TravelAndTours.Application.Common.Errors;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IUnitOfWork _uow;

    public UpdateProductCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken ct)
    {
        var entity = await _uow.Products.GetByIdAsync(request.Id, ct);
        if (entity is null)
            throw new NotFoundException($"Product {request.Id} not found.");

        entity.Name = request.Name.Trim();
        entity.Price = request.Price;
        entity.UpdatedAt = DateTime.UtcNow;

        _uow.Products.Update(entity);
        await _uow.SaveChangesAsync(ct);

        return true;
    }
}
