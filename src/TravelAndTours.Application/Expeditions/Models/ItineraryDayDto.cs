namespace TravelAndTours.Application.Expeditions.Models;

public sealed record ItineraryDayDto(
    int Id,
    int DayNumber,
    string Title,
    string DescriptionMarkdown,
    string? Meals,
    string? Accommodation,
    int? ElevationMeters);
