using FluentValidation;

namespace TravelAndTours.Application.Expeditions.Queries.GetAdminExpeditions;

public sealed class GetAdminExpeditionsQueryValidator : AbstractValidator<GetAdminExpeditionsQuery>
{
    public GetAdminExpeditionsQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);
    }
}
