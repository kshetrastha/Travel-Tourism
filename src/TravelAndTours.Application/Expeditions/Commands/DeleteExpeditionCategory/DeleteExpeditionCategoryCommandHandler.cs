using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelAndTours.Application.Common.Errors;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Commands.DeleteExpeditionCategory;

public sealed class DeleteExpeditionCategoryCommandHandler : IRequestHandler<DeleteExpeditionCategoryCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public DeleteExpeditionCategoryCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<bool>> Handle(DeleteExpeditionCategoryCommand request, CancellationToken ct)
    {
        var category = await _uow.ExpeditionCategories.GetByIdAsync(request.Id, ct);
        if (category is null)
        {
            throw new NotFoundException("Expedition category not found.");
        }

        var hasChildren = await _uow.ExpeditionCategories.Query()
            .AnyAsync(x => x.ParentCategoryId == request.Id, ct);
        if (hasChildren)
        {
            return ApiResponse<bool>.Fail("Category has child categories. Remove them first.");
        }

        var hasExpeditions = await _uow.Expeditions.Query()
            .AnyAsync(x => x.CategoryId == request.Id, ct);
        if (hasExpeditions)
        {
            return ApiResponse<bool>.Fail("Category is assigned to expeditions. Reassign them first.");
        }

        _uow.ExpeditionCategories.Remove(category);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Expedition category deleted.");
    }
}
