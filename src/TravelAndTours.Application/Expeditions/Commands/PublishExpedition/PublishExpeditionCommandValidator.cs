using FluentValidation;

namespace TravelAndTours.Application.Expeditions.Commands.PublishExpedition;

public sealed class PublishExpeditionCommandValidator : AbstractValidator<PublishExpeditionCommand>
{
    public PublishExpeditionCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
