using MediatR;
using TravelAndTours.Application.Common.Errors;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Domain.Entities;
using TravelAndTours.Domain.Enums;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Commands.ReplaceFixedDepartures;

public sealed class ReplaceFixedDeparturesCommandHandler : IRequestHandler<ReplaceFixedDeparturesCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public ReplaceFixedDeparturesCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<bool>> Handle(ReplaceFixedDeparturesCommand request, CancellationToken ct)
    {
        var expedition = await _uow.Expeditions.GetByIdAsync(request.ExpeditionId, ct);
        if (expedition is null)
        {
            throw new NotFoundException("Expedition not found.");
        }

        var existing = _uow.FixedDepartures.Query()
            .Where(x => x.ExpeditionId == expedition.Id)
            .ToList();

        foreach (var departure in existing)
        {
            _uow.FixedDepartures.Remove(departure);
        }

        foreach (var departure in request.Departures)
        {
            if (!Enum.TryParse<FixedDepartureStatus>(departure.Status, true, out var status))
            {
                return ApiResponse<bool>.Fail($"Invalid fixed departure status '{departure.Status}'.");
            }

            await _uow.FixedDepartures.AddAsync(new FixedDeparture
            {
                ExpeditionId = expedition.Id,
                VariantId = departure.VariantId,
                StartDate = departure.StartDate,
                EndDate = departure.EndDate,
                Price = departure.Price,
                Currency = departure.Currency.Trim().ToUpperInvariant(),
                SlotsTotal = departure.SlotsTotal,
                SlotsAvailable = departure.SlotsAvailable,
                Status = status,
                Notes = departure.Notes?.Trim(),
                CreatedAt = DateTime.UtcNow
            }, ct);
        }

        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Fixed departures updated.");
    }
}
