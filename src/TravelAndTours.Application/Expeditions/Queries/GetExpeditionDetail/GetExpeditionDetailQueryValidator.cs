using FluentValidation;

namespace TravelAndTours.Application.Expeditions.Queries.GetExpeditionDetail;

public sealed class GetExpeditionDetailQueryValidator : AbstractValidator<GetExpeditionDetailQuery>
{
    public GetExpeditionDetailQueryValidator()
    {
        RuleFor(x => x.Slug)
            .NotEmpty()
            .MaximumLength(200);
    }
}
