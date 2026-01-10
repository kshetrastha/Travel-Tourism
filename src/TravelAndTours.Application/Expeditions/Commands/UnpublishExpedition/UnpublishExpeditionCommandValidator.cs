using FluentValidation;

namespace TravelAndTours.Application.Expeditions.Commands.UnpublishExpedition;

public sealed class UnpublishExpeditionCommandValidator : AbstractValidator<UnpublishExpeditionCommand>
{
    public UnpublishExpeditionCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
