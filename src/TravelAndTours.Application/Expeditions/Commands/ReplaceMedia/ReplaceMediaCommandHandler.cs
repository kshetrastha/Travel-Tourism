using MediatR;
using TravelAndTours.Application.Common.Errors;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Domain.Entities;
using TravelAndTours.Domain.Enums;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Commands.ReplaceMedia;

public sealed class ReplaceMediaCommandHandler : IRequestHandler<ReplaceMediaCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public ReplaceMediaCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<bool>> Handle(ReplaceMediaCommand request, CancellationToken ct)
    {
        var expedition = await _uow.Expeditions.GetByIdAsync(request.ExpeditionId, ct);
        if (expedition is null)
        {
            throw new NotFoundException("Expedition not found.");
        }

        var existing = _uow.MediaAssets.Query()
            .Where(x => x.ExpeditionId == expedition.Id)
            .ToList();

        foreach (var asset in existing)
        {
            _uow.MediaAssets.Remove(asset);
        }

        foreach (var asset in request.Media)
        {
            if (!Enum.TryParse<MediaType>(asset.MediaType, true, out var mediaType))
            {
                return ApiResponse<bool>.Fail($"Invalid media type '{asset.MediaType}'.");
            }

            await _uow.MediaAssets.AddAsync(new MediaAsset
            {
                ExpeditionId = expedition.Id,
                MediaType = mediaType,
                Url = asset.Url.Trim(),
                ThumbnailUrl = asset.ThumbnailUrl?.Trim(),
                Title = asset.Title?.Trim(),
                SortOrder = asset.SortOrder,
                CreatedAt = DateTime.UtcNow
            }, ct);
        }

        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Media updated.");
    }
}
