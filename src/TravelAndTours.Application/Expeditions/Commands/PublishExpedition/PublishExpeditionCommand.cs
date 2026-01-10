using MediatR;
using TravelAndTours.Application.Common.Models;

namespace TravelAndTours.Application.Expeditions.Commands.PublishExpedition;

public sealed record PublishExpeditionCommand(int Id) : IRequest<ApiResponse<bool>>;
