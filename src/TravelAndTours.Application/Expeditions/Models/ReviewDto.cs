namespace TravelAndTours.Application.Expeditions.Models;

public sealed record ReviewDto(
    int Id,
    string? ReviewerName,
    int Rating,
    string Comment,
    DateTime CreatedAt,
    bool IsApproved);
