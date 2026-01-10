using MediatR;
using TravelAndTours.Application.Common.Errors;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Domain.Enums;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Commands.PublishExpedition;

public sealed class PublishExpeditionCommandHandler : IRequestHandler<PublishExpeditionCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public PublishExpeditionCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<bool>> Handle(PublishExpeditionCommand request, CancellationToken ct)
    {
        var expedition = await _uow.Expeditions.GetByIdAsync(request.Id, ct);
        if (expedition is null)
        {
            throw new NotFoundException("Expedition not found.");
        }

        if (string.IsNullOrWhiteSpace(expedition.OverviewMarkdown)
            || string.IsNullOrWhiteSpace(expedition.IncludesMarkdown)
            || string.IsNullOrWhiteSpace(expedition.ExcludesMarkdown))
        {
            return ApiResponse<bool>.Fail("Overview, includes, and excludes must be provided before publishing.");
        }

        var hasFacts = _uow.ExpeditionFacts.Query().Any(x => x.ExpeditionId == expedition.Id);
        var hasVariants = _uow.ExpeditionVariants.Query().Any(x => x.ExpeditionId == expedition.Id && x.IsActive);
        var hasItinerary = _uow.ItineraryDays.Query().Any(x => x.ExpeditionId == expedition.Id);
        var hasFixedDepartures = _uow.FixedDepartures.Query().Any(x => x.ExpeditionId == expedition.Id);
        var hasMedia = _uow.MediaAssets.Query().Any(x => x.ExpeditionId == expedition.Id);

        if (!hasFacts || !hasVariants || !hasItinerary || !hasFixedDepartures || !hasMedia)
        {
            return ApiResponse<bool>.Fail("Expedition must include facts, variants, itinerary, fixed departures, and media before publishing.");
        }

        expedition.Status = ExpeditionStatus.Published;
        expedition.PublishedAt = DateTime.UtcNow;
        expedition.UpdatedAt = DateTime.UtcNow;

        _uow.Expeditions.Update(expedition);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Expedition published.");
    }
}
