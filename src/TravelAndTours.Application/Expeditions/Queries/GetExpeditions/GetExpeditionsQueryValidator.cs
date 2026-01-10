using FluentValidation;

namespace TravelAndTours.Application.Expeditions.Queries.GetExpeditions;

public sealed class GetExpeditionsQueryValidator : AbstractValidator<GetExpeditionsQuery>
{
    public GetExpeditionsQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);
    }
}
