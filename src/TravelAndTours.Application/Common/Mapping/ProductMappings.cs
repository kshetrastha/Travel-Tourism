using TravelAndTours.Application.Products.Models;
using TravelAndTours.Domain.Entities;

namespace TravelAndTours.Application.Common.Mapping;

public static class ProductMappings
{
    public static ProductDto ToDto(this Product entity) =>
        new(entity.Id, entity.Name, entity.Price, entity.CreatedAt, entity.UpdatedAt);
}
