using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Domain.Enums;

namespace TravelAndTours.Application.Expeditions.Commands.ReplaceMedia;

public sealed record ReplaceMediaCommand(
    int ExpeditionId,
    IReadOnlyList<MediaAssetInput> Media,
    ExpeditionType Type = ExpeditionType.Expedition) : IRequest<ApiResponse<bool>>;
