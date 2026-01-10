using MediatR;
using TravelAndTours.Application.Common.Errors;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Domain.Entities;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Commands.ReplaceItinerary;

public sealed class ReplaceItineraryCommandHandler : IRequestHandler<ReplaceItineraryCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public ReplaceItineraryCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<bool>> Handle(ReplaceItineraryCommand request, CancellationToken ct)
    {
        var expedition = await _uow.Expeditions.GetByIdAsync(request.ExpeditionId, ct);
        if (expedition is null)
        {
            throw new NotFoundException("Expedition not found.");
        }

        var existing = _uow.ItineraryDays.Query()
            .Where(x => x.ExpeditionId == expedition.Id)
            .ToList();

        foreach (var day in existing)
        {
            _uow.ItineraryDays.Remove(day);
        }

        foreach (var day in request.Days)
        {
            await _uow.ItineraryDays.AddAsync(new ItineraryDay
            {
                ExpeditionId = expedition.Id,
                DayNumber = day.DayNumber,
                Title = day.Title.Trim(),
                DescriptionMarkdown = day.DescriptionMarkdown,
                Meals = day.Meals?.Trim(),
                Accommodation = day.Accommodation?.Trim(),
                ElevationMeters = day.ElevationMeters,
                CreatedAt = DateTime.UtcNow
            }, ct);
        }

        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Itinerary updated.");
    }
}
