namespace TravelAndTours.Application.Expeditions.Models;

public sealed record FaqItemDto(
    int Id,
    string Question,
    string AnswerMarkdown,
    int SortOrder,
    bool IsActive);
