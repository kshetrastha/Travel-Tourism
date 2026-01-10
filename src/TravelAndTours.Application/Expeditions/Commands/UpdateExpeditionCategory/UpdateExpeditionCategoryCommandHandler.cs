using MediatR;
using TravelAndTours.Application.Common.Errors;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Commands.UpdateExpeditionCategory;

public sealed class UpdateExpeditionCategoryCommandHandler : IRequestHandler<UpdateExpeditionCategoryCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public UpdateExpeditionCategoryCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<bool>> Handle(UpdateExpeditionCategoryCommand request, CancellationToken ct)
    {
        var entity = await _uow.ExpeditionCategories.GetByIdAsync(request.Id, ct);
        if (entity is null)
        {
            throw new NotFoundException("Expedition category not found.");
        }

        var normalizedSlug = request.Slug.Trim();
        if (!string.Equals(entity.Slug, normalizedSlug, StringComparison.OrdinalIgnoreCase)
            && await _uow.ExpeditionCategories.ExistsBySlugAsync(normalizedSlug, ct))
        {
            return ApiResponse<bool>.Fail($"Category slug '{normalizedSlug}' already exists.");
        }

        entity.Name = request.Name.Trim();
        entity.Slug = normalizedSlug;
        entity.ParentCategoryId = request.ParentCategoryId;
        entity.SortOrder = request.SortOrder;
        entity.IsActive = request.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        _uow.ExpeditionCategories.Update(entity);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Expedition category updated.");
    }
}
