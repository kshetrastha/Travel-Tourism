using MediatR;
using TravelAndTours.Application.Common.Models;

namespace TravelAndTours.Application.Expeditions.Commands.UnpublishExpedition;

public sealed record UnpublishExpeditionCommand(int Id) : IRequest<ApiResponse<bool>>;
