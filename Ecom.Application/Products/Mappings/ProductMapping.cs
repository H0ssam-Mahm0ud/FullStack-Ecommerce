using Ecom.Application.Photos.Dtos;
using Ecom.Application.Photos.Mappings;
using Ecom.Application.Products.Dtos;
using Ecom.Domain.Entities.Product;

namespace Ecom.Application.Products.Mappings;

public static class ProductMapping
{
    public static ProductDto ToDto(this Product input)
    {
        return new ProductDto
        {
            Id = input.Id,
            Name = input.Name,
            Description = input.Description,
            NewPrice = input.NewPrice,
            OldPrice = input.OldPrice,
            CategoryId = input.CategoryId,
            Images = input.Images?.Select(i => i.ToDto()).ToList() ?? new List<ImageDto>()
        };
    }

    public static Product ToEntity(this CreateUpdateProductDto input)
    {
        return new Product
        {
            Name = input.Name,
            Description = input.Description,
            NewPrice = input.NewPrice,
            OldPrice = input.OldPrice,
            CategoryId = input.CategoryId,
            Images = new List<Image>()
        };
    }

    public static void MapTo(this CreateUpdateProductDto dto, Product entity)
    {
        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.NewPrice = dto.NewPrice;
        entity.OldPrice = dto.OldPrice;
        entity.CategoryId = dto.CategoryId;
    }

    public static Product UpdateEntity(this Product entity, CreateUpdateProductDto dto)
    {
        dto.MapTo(entity);
        return entity;
    }
}
