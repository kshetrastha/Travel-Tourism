using FluentValidation;

namespace TravelAndTours.Application.Expeditions.Commands.DeleteExpedition;

public sealed class DeleteExpeditionCommandValidator : AbstractValidator<DeleteExpeditionCommand>
{
    public DeleteExpeditionCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
