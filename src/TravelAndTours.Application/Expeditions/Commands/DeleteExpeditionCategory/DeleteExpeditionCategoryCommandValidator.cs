using FluentValidation;

namespace TravelAndTours.Application.Expeditions.Commands.DeleteExpeditionCategory;

public sealed class DeleteExpeditionCategoryCommandValidator : AbstractValidator<DeleteExpeditionCategoryCommand>
{
    public DeleteExpeditionCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
