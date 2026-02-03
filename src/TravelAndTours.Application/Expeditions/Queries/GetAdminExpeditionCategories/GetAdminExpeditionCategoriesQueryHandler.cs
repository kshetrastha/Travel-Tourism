using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Queries.GetAdminExpeditionCategories;

public sealed class GetAdminExpeditionCategoriesQueryHandler : IRequestHandler<GetAdminExpeditionCategoriesQuery, IReadOnlyList<ExpeditionCategoryDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAdminExpeditionCategoriesQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IReadOnlyList<ExpeditionCategoryDto>> Handle(GetAdminExpeditionCategoriesQuery request, CancellationToken ct)
    {
        var categories = await _uow.ExpeditionCategories.Query()
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Name)
            .Select(x => new ExpeditionCategoryDto(
                x.Id,
                x.Name,
                x.Slug,
                x.ParentCategoryId,
                x.SortOrder,
                x.IsActive))
            .ToListAsync(ct);

        return categories.AsReadOnly();
    }
}
