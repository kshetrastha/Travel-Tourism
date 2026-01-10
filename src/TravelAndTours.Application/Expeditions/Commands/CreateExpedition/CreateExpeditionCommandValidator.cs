using FluentValidation;

namespace TravelAndTours.Application.Expeditions.Commands.CreateExpedition;

public sealed class CreateExpeditionCommandValidator : AbstractValidator<CreateExpeditionCommand>
{
    public CreateExpeditionCommandValidator()
    {
        RuleFor(x => x.Model.CategoryId)
            .GreaterThan(0);

        RuleFor(x => x.Model.Title)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.Model.Slug)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Model.DurationDays)
            .GreaterThan(0);

        RuleFor(x => x.Model.MaxAltitudeMeters)
            .GreaterThan(0);

        RuleFor(x => x.Model.Difficulty)
            .NotEmpty();

        RuleFor(x => x.Model.Region)
            .NotEmpty()
            .MaximumLength(120);

        RuleFor(x => x.Model.Country)
            .NotEmpty()
            .MaximumLength(120);

        RuleFor(x => x.Model.BestSeason)
            .NotEmpty()
            .MaximumLength(120);

        RuleFor(x => x.Model.OverviewMarkdown)
            .NotEmpty();

        RuleFor(x => x.Model.IncludesMarkdown)
            .NotEmpty();

        RuleFor(x => x.Model.ExcludesMarkdown)
            .NotEmpty();

        RuleForEach(x => x.Model.Facts)
            .ChildRules(fact =>
            {
                fact.RuleFor(x => x.Label)
                    .NotEmpty()
                    .MaximumLength(120);
                fact.RuleFor(x => x.Value)
                    .NotEmpty()
                    .MaximumLength(200);
            });

        RuleForEach(x => x.Model.Variants)
            .ChildRules(variant =>
            {
                variant.RuleFor(x => x.VariantType)
                    .NotEmpty();
                variant.RuleFor(x => x.PriceFrom)
                    .GreaterThan(0);
            });
    }
}
