using FluentValidation;
using MediatR;
using TravelAndTours.Domain.Entities;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Products.Commands.CreateProduct;

public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IUnitOfWork _uow;

    public CreateProductCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken ct)
    {
        if (await _uow.Products.ExistsByNameAsync(request.Name, ct))
            throw new ValidationException($"Product with name '{request.Name}' already exists.");

        var entity = new Product
        {
            Name = request.Name.Trim(),
            Price = request.Price,
            CreatedAt = DateTime.UtcNow
        };

        await _uow.Products.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        return entity.Id;
    }
}
