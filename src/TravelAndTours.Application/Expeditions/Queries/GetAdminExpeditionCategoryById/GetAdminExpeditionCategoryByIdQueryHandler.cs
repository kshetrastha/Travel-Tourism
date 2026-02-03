using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Queries.GetAdminExpeditionCategoryById;

public sealed class GetAdminExpeditionCategoryByIdQueryHandler : IRequestHandler<GetAdminExpeditionCategoryByIdQuery, ExpeditionCategoryDto?>
{
    private readonly IUnitOfWork _uow;

    public GetAdminExpeditionCategoryByIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<ExpeditionCategoryDto?> Handle(GetAdminExpeditionCategoryByIdQuery request, CancellationToken ct)
        => _uow.ExpeditionCategories.Query()
            .Where(x => x.Id == request.Id)
            .Select(x => new ExpeditionCategoryDto(
                x.Id,
                x.Name,
                x.Slug,
                x.ParentCategoryId,
                x.SortOrder,
                x.IsActive))
            .FirstOrDefaultAsync(ct);
}
