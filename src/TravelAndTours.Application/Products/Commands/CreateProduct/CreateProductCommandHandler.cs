using FluentValidation;
using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Domain.Entities;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Products.Commands.CreateProduct;
public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponse<int>>
{
    private readonly IUnitOfWork _uow;

    public CreateProductCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<int>> Handle(
        CreateProductCommand request,
        CancellationToken ct)
    {
        if (await _uow.Products.ExistsByNameAsync(request.Name, ct))
        {
            return ApiResponse<int>.Fail(
                $"Product with name '{request.Name}' already exists.");
        }

        var entity = new Product
        {
            Name = request.Name.Trim(),
            Price = request.Price,
            CreatedAt = DateTime.UtcNow
        };

        await _uow.Products.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<int>.Ok(
            entity.Id,
            "Product created successfully");
    }

}
