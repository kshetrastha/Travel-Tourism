namespace TravelAndTours.Application.Expeditions.Models;

public sealed record ReplaceItineraryRequest(IReadOnlyList<ItineraryDayInput> Days);

public sealed record ReplaceFixedDeparturesRequest(IReadOnlyList<FixedDepartureInput> Departures);

public sealed record ReplaceMediaRequest(IReadOnlyList<MediaAssetInput> Media);
