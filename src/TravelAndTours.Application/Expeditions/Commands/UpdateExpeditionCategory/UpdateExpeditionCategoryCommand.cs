using MediatR;
using TravelAndTours.Application.Common.Models;

namespace TravelAndTours.Application.Expeditions.Commands.UpdateExpeditionCategory;

public sealed record UpdateExpeditionCategoryCommand(
    int Id,
    string Name,
    string Slug,
    int? ParentCategoryId,
    int SortOrder,
    bool IsActive) : IRequest<ApiResponse<bool>>;
