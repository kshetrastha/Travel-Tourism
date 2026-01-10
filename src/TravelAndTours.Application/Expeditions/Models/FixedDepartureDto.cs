namespace TravelAndTours.Application.Expeditions.Models;

public sealed record FixedDepartureDto(
    int Id,
    DateTime StartDate,
    DateTime EndDate,
    decimal Price,
    string Currency,
    int SlotsTotal,
    int SlotsAvailable,
    string Status,
    string? Notes,
    int? VariantId);
