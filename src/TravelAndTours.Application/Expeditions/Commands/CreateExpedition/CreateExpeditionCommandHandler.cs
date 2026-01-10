using MediatR;
using TravelAndTours.Application.Common.Models;
using TravelAndTours.Application.Expeditions.Models;
using TravelAndTours.Domain.Entities;
using TravelAndTours.Domain.Enums;
using TravelAndTours.Domain.Interfaces;

namespace TravelAndTours.Application.Expeditions.Commands.CreateExpedition;

public sealed class CreateExpeditionCommandHandler : IRequestHandler<CreateExpeditionCommand, ApiResponse<int>>
{
    private readonly IUnitOfWork _uow;

    public CreateExpeditionCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<int>> Handle(CreateExpeditionCommand request, CancellationToken ct)
    {
        var model = request.Model;
        var normalizedSlug = model.Slug.Trim();

        if (await _uow.Expeditions.ExistsBySlugAsync(normalizedSlug, ct))
        {
            return ApiResponse<int>.Fail($"Expedition slug '{normalizedSlug}' already exists.");
        }

        var category = await _uow.ExpeditionCategories.GetByIdAsync(model.CategoryId, ct);
        if (category is null)
        {
            return ApiResponse<int>.Fail("Category not found.");
        }

        if (!Enum.TryParse<DifficultyLevel>(model.Difficulty, true, out var difficulty))
        {
            return ApiResponse<int>.Fail("Invalid difficulty value.");
        }

        var expedition = new Expedition
        {
            CategoryId = model.CategoryId,
            Title = model.Title.Trim(),
            Slug = normalizedSlug,
            ShortTitle = model.ShortTitle?.Trim(),
            Tagline = model.Tagline?.Trim(),
            DurationDays = model.DurationDays,
            MaxAltitudeMeters = model.MaxAltitudeMeters,
            Difficulty = difficulty,
            Region = model.Region.Trim(),
            Country = model.Country.Trim(),
            BestSeason = model.BestSeason.Trim(),
            GroupSizeMin = model.GroupSizeMin,
            GroupSizeMax = model.GroupSizeMax,
            StartingPoint = model.StartingPoint?.Trim(),
            EndingPoint = model.EndingPoint?.Trim(),
            OverviewMarkdown = model.OverviewMarkdown,
            IncludesMarkdown = model.IncludesMarkdown,
            ExcludesMarkdown = model.ExcludesMarkdown,
            Status = ExpeditionStatus.Draft,
            CreatedAt = DateTime.UtcNow
        };

        foreach (var fact in model.Facts)
        {
            expedition.Facts.Add(new ExpeditionFact
            {
                Label = fact.Label.Trim(),
                Value = fact.Value.Trim(),
                SortOrder = fact.SortOrder,
                CreatedAt = DateTime.UtcNow
            });
        }

        foreach (var variant in model.Variants)
        {
            if (!Enum.TryParse<ExpeditionVariantType>(variant.VariantType, true, out var variantType))
            {
                return ApiResponse<int>.Fail($"Invalid variant type '{variant.VariantType}'.");
            }

            expedition.Variants.Add(new ExpeditionVariant
            {
                VariantType = variantType,
                TitleOverride = variant.TitleOverride?.Trim(),
                PriceFrom = variant.PriceFrom,
                IsActive = variant.IsActive,
                InclusionsOverrideMarkdown = variant.InclusionsOverrideMarkdown,
                ExclusionsOverrideMarkdown = variant.ExclusionsOverrideMarkdown,
                CreatedAt = DateTime.UtcNow
            });
        }

        await _uow.Expeditions.AddAsync(expedition, ct);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<int>.Ok(expedition.Id, "Expedition created as draft.");
    }
}
