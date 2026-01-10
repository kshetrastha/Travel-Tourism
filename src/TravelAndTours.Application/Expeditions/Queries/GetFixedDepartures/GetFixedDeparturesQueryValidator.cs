using FluentValidation;

namespace TravelAndTours.Application.Expeditions.Queries.GetFixedDepartures;

public sealed class GetFixedDeparturesQueryValidator : AbstractValidator<GetFixedDeparturesQuery>
{
    public GetFixedDeparturesQueryValidator()
    {
        RuleFor(x => x.Slug)
            .NotEmpty()
            .MaximumLength(200);
    }
}
