using MediatR;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Queries.GetExpeditionCategories;

public sealed class GetExpeditionCategoriesQueryHandler : IRequestHandler<GetExpeditionCategoriesQuery, IReadOnlyList<ExpeditionCategoryDto>>
{
    private readonly IUnitOfWork _uow;

    public GetExpeditionCategoriesQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<IReadOnlyList<ExpeditionCategoryDto>> Handle(GetExpeditionCategoriesQuery request, CancellationToken ct)
    {
        var categories = _uow.ExpeditionCategories.Query()
            .Where(x => x.IsActive)
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Name)
            .Select(x => new ExpeditionCategoryDto(
                x.Id,
                x.Name,
                x.Slug,
                x.ParentCategoryId,
                x.SortOrder,
                x.IsActive))
            .ToList();

        return Task.FromResult<IReadOnlyList<ExpeditionCategoryDto>>(categories);
    }
}
