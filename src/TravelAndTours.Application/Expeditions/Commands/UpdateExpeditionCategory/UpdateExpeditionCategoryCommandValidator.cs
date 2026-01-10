using FluentValidation;

namespace TravelAndTours.Application.Expeditions.Commands.UpdateExpeditionCategory;

public sealed class UpdateExpeditionCategoryCommandValidator : AbstractValidator<UpdateExpeditionCategoryCommand>
{
    public UpdateExpeditionCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

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
