using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Expeditions.Models;

namespace TravelAndTours.Application.Expeditions.Commands.CreateExpedition;

public sealed record CreateExpeditionCommand(ExpeditionUpsertModel Model) : IRequest<ApiResponse<int>>;
