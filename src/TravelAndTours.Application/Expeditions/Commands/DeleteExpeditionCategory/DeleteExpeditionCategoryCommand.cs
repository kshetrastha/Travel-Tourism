using MediatR;
using TravelAndTours.Application.Common.Models;

namespace TravelAndTours.Application.Expeditions.Commands.DeleteExpeditionCategory;

public sealed record DeleteExpeditionCategoryCommand(int Id) : IRequest<ApiResponse<bool>>;
