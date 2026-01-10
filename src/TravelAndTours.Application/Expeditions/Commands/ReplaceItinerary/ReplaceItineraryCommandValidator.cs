using FluentValidation;

namespace TravelAndTours.Application.Expeditions.Commands.ReplaceItinerary;

public sealed class ReplaceItineraryCommandValidator : AbstractValidator<ReplaceItineraryCommand>
{
    public ReplaceItineraryCommandValidator()
    {
        RuleFor(x => x.ExpeditionId)
            .GreaterThan(0);

        RuleForEach(x => x.Days)
            .ChildRules(day =>
            {
                day.RuleFor(x => x.DayNumber)
                    .GreaterThan(0);
                day.RuleFor(x => x.Title)
                    .NotEmpty()
                    .MaximumLength(200);
                day.RuleFor(x => x.DescriptionMarkdown)
                    .NotEmpty();
            });
    }
}
