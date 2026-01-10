using FluentValidation;

namespace TravelAndTours.Application.Expeditions.Commands.ReplaceFixedDepartures;

public sealed class ReplaceFixedDeparturesCommandValidator : AbstractValidator<ReplaceFixedDeparturesCommand>
{
    public ReplaceFixedDeparturesCommandValidator()
    {
        RuleFor(x => x.ExpeditionId)
            .GreaterThan(0);

        RuleForEach(x => x.Departures)
            .ChildRules(dep =>
            {
                dep.RuleFor(x => x.StartDate)
                    .NotEmpty();
                dep.RuleFor(x => x.EndDate)
                    .NotEmpty();
                dep.RuleFor(x => x.Price)
                    .GreaterThan(0);
                dep.RuleFor(x => x.Currency)
                    .NotEmpty()
                    .Length(3);
                dep.RuleFor(x => x.SlotsTotal)
                    .GreaterThan(0);
                dep.RuleFor(x => x.SlotsAvailable)
                    .GreaterThanOrEqualTo(0);
                dep.RuleFor(x => x.Status)
                    .NotEmpty();
            });
    }
}
