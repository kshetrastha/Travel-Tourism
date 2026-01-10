using MediatR;
using TravelAndTours.Application.Common.Models;

namespace TravelAndTours.Application.Expeditions.Commands.CreateExpeditionCategory;

public sealed record CreateExpeditionCategoryCommand(
    string Name,
    string Slug,
    int? ParentCategoryId,
    int SortOrder,
    bool IsActive) : IRequest<ApiResponse<int>>;
