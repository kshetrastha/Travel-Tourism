using MediatR;
using TravelAndTours.Application.Common.Errors;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Domain.Enums;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Commands.UnpublishExpedition;

public sealed class UnpublishExpeditionCommandHandler : IRequestHandler<UnpublishExpeditionCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public UnpublishExpeditionCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<bool>> Handle(UnpublishExpeditionCommand request, CancellationToken ct)
    {
        var expedition = await _uow.Expeditions.GetByIdAsync(request.Id, ct);
        if (expedition is null)
        {
            throw new NotFoundException("Expedition not found.");
        }

        if (expedition.Type != request.Type)
        {
            throw new NotFoundException($"{request.Type} not found.");
        }

        expedition.Status = ExpeditionStatus.Draft;
        expedition.PublishedAt = null;
        expedition.UpdatedAt = DateTime.UtcNow;

        _uow.Expeditions.Update(expedition);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Expedition unpublished.");
    }
}
