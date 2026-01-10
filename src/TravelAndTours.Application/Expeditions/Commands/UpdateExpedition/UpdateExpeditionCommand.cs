using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Expeditions.Models;

namespace TravelAndTours.Application.Expeditions.Commands.UpdateExpedition;

public sealed record UpdateExpeditionCommand(int Id, ExpeditionUpsertModel Model) : IRequest<ApiResponse<bool>>;
