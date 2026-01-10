using MediatR;
using TravelAndTours.Application.Common.Errors;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Domain.Entities;
using TravelAndTours.Domain.Enums;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Commands.UpdateExpedition;

public sealed class UpdateExpeditionCommandHandler : IRequestHandler<UpdateExpeditionCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public UpdateExpeditionCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<bool>> Handle(UpdateExpeditionCommand request, CancellationToken ct)
    {
        var expedition = await _uow.Expeditions.GetByIdAsync(request.Id, ct);
        if (expedition is null)
        {
            throw new NotFoundException("Expedition not found.");
        }

        var model = request.Model;
        var normalizedSlug = model.Slug.Trim();

        if (!string.Equals(expedition.Slug, normalizedSlug, StringComparison.OrdinalIgnoreCase)
            && await _uow.Expeditions.ExistsBySlugAsync(normalizedSlug, ct))
        {
            return ApiResponse<bool>.Fail($"Expedition slug '{normalizedSlug}' already exists.");
        }

        var category = await _uow.ExpeditionCategories.GetByIdAsync(model.CategoryId, ct);
        if (category is null)
        {
            return ApiResponse<bool>.Fail("Category not found.");
        }

        if (!Enum.TryParse<DifficultyLevel>(model.Difficulty, true, out var difficulty))
        {
            return ApiResponse<bool>.Fail("Invalid difficulty value.");
        }

        expedition.CategoryId = model.CategoryId;
        expedition.Title = model.Title.Trim();
        expedition.Slug = normalizedSlug;
        expedition.ShortTitle = model.ShortTitle?.Trim();
        expedition.Tagline = model.Tagline?.Trim();
        expedition.DurationDays = model.DurationDays;
        expedition.MaxAltitudeMeters = model.MaxAltitudeMeters;
        expedition.Difficulty = difficulty;
        expedition.Region = model.Region.Trim();
        expedition.Country = model.Country.Trim();
        expedition.BestSeason = model.BestSeason.Trim();
        expedition.GroupSizeMin = model.GroupSizeMin;
        expedition.GroupSizeMax = model.GroupSizeMax;
        expedition.StartingPoint = model.StartingPoint?.Trim();
        expedition.EndingPoint = model.EndingPoint?.Trim();
        expedition.OverviewMarkdown = model.OverviewMarkdown;
        expedition.IncludesMarkdown = model.IncludesMarkdown;
        expedition.ExcludesMarkdown = model.ExcludesMarkdown;
        expedition.UpdatedAt = DateTime.UtcNow;

        var existingFacts = _uow.ExpeditionFacts.Query().Where(x => x.ExpeditionId == expedition.Id).ToList();
        foreach (var fact in existingFacts)
        {
            _uow.ExpeditionFacts.Remove(fact);
        }

        foreach (var fact in model.Facts)
        {
            await _uow.ExpeditionFacts.AddAsync(new ExpeditionFact
            {
                ExpeditionId = expedition.Id,
                Label = fact.Label.Trim(),
                Value = fact.Value.Trim(),
                SortOrder = fact.SortOrder,
                CreatedAt = DateTime.UtcNow
            }, ct);
        }

        var existingVariants = _uow.ExpeditionVariants.Query().Where(x => x.ExpeditionId == expedition.Id).ToList();
        foreach (var variant in existingVariants)
        {
            _uow.ExpeditionVariants.Remove(variant);
        }

        foreach (var variant in model.Variants)
        {
            if (!Enum.TryParse<ExpeditionVariantType>(variant.VariantType, true, out var variantType))
            {
                return ApiResponse<bool>.Fail($"Invalid variant type '{variant.VariantType}'.");
            }

            await _uow.ExpeditionVariants.AddAsync(new ExpeditionVariant
            {
                ExpeditionId = expedition.Id,
                VariantType = variantType,
                TitleOverride = variant.TitleOverride?.Trim(),
                PriceFrom = variant.PriceFrom,
                IsActive = variant.IsActive,
                InclusionsOverrideMarkdown = variant.InclusionsOverrideMarkdown,
                ExclusionsOverrideMarkdown = variant.ExclusionsOverrideMarkdown,
                CreatedAt = DateTime.UtcNow
            }, ct);
        }

        _uow.Expeditions.Update(expedition);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Expedition updated.");
    }
}
