using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Expeditions.Models;

namespace TravelAndTours.Application.Expeditions.Commands.ReplaceMedia;

public sealed record ReplaceMediaCommand(int ExpeditionId, IReadOnlyList<MediaAssetInput> Media) : IRequest<ApiResponse<bool>>;
