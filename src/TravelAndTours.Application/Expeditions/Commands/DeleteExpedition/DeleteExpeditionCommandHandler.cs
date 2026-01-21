using MediatR;
using TravelAndTours.Application.Common.Errors;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Commands.DeleteExpedition;

public sealed class DeleteExpeditionCommandHandler : IRequestHandler<DeleteExpeditionCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public DeleteExpeditionCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<bool>> Handle(DeleteExpeditionCommand request, CancellationToken ct)
    {
        var expedition = await _uow.Expeditions.GetByIdAsync(request.Id, ct);
        if (expedition is null)
        {
            throw new NotFoundException("Expedition not found.");
        }

        _uow.Expeditions.Remove(expedition);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Expedition deleted.");
    }
}
