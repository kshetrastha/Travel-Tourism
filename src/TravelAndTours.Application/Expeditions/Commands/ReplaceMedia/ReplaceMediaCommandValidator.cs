using FluentValidation;

namespace TravelAndTours.Application.Expeditions.Commands.ReplaceMedia;

public sealed class ReplaceMediaCommandValidator : AbstractValidator<ReplaceMediaCommand>
{
    public ReplaceMediaCommandValidator()
    {
        RuleFor(x => x.ExpeditionId)
            .GreaterThan(0);

        RuleForEach(x => x.Media)
            .ChildRules(media =>
            {
                media.RuleFor(x => x.MediaType)
                    .NotEmpty();
                media.RuleFor(x => x.Url)
                    .NotEmpty()
                    .MaximumLength(500);
                media.RuleFor(x => x.SortOrder)
                    .GreaterThanOrEqualTo(0);
            });
    }
}
