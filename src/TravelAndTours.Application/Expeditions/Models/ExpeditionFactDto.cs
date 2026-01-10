namespace TravelAndTours.Application.Expeditions.Models;

public sealed record ExpeditionFactDto(
    int Id,
    string Label,
    string Value,
    int SortOrder);
