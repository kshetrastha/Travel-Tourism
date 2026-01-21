using MediatR;
using TravelAndTours.Application.Common.Models;

namespace TravelAndTours.Application.Expeditions.Commands.DeleteExpedition;

public sealed record DeleteExpeditionCommand(int Id) : IRequest<ApiResponse<bool>>;
