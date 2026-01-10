using MediatR;
using TravelAndTours.Application.Common.Errors;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Products.Commands.DeleteProduct;

public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IUnitOfWork _uow;

    public DeleteProductCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken ct)
    {
        var entity = await _uow.Products.GetByIdAsync(request.Id, ct);
        if (entity is null)
            throw new NotFoundException($"Product {request.Id} not found.");

        _uow.Products.Remove(entity);
        await _uow.SaveChangesAsync(ct);

        return true;
    }
}
