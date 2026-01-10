using FluentValidation;

namespace TravelAndTours.Application.Expeditions.Commands.CreateExpeditionCategory;

public sealed class CreateExpeditionCategoryCommandValidator : AbstractValidator<CreateExpeditionCategoryCommand>
{
    public CreateExpeditionCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Slug)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.SortOrder)
            .GreaterThanOrEqualTo(0);
    }
}
