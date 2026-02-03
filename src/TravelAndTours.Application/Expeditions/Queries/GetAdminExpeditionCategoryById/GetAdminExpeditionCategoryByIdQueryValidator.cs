using FluentValidation;

namespace TravelAndTours.Application.Expeditions.Queries.GetAdminExpeditionCategoryById;

public sealed class GetAdminExpeditionCategoryByIdQueryValidator : AbstractValidator<GetAdminExpeditionCategoryByIdQuery>
{
    public GetAdminExpeditionCategoryByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
