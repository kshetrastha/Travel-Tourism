using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Domain.Entities;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Commands.CreateExpeditionCategory;

public sealed class CreateExpeditionCategoryCommandHandler : IRequestHandler<CreateExpeditionCategoryCommand, ApiResponse<int>>
{
    private readonly IUnitOfWork _uow;

    public CreateExpeditionCategoryCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<int>> Handle(CreateExpeditionCategoryCommand request, CancellationToken ct)
    {
        var normalizedSlug = request.Slug.Trim();
        if (await _uow.ExpeditionCategories.ExistsBySlugAsync(normalizedSlug, ct))
        {
            return ApiResponse<int>.Fail($"Category slug '{normalizedSlug}' already exists.");
        }

        var entity = new ExpeditionCategory
        {
            Name = request.Name.Trim(),
            Slug = normalizedSlug,
            ParentCategoryId = request.ParentCategoryId,
            SortOrder = request.SortOrder,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        await _uow.ExpeditionCategories.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<int>.Ok(entity.Id, "Expedition category created.");
    }
}
