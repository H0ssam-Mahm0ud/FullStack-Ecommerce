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
            Price = input.Price,
            CategoryId = input.CategoryId,
            Images = input.Images?.Select(p => p.ToDto()).ToList() ?? new List<ImageDto>()
        };
    }

    public static Product ToEntity(this CreateUpdateProductDto input)
    {
        return new Product
        {
            Name = input.Name,
            Description = input.Description,
            Price = input.Price,
            CategoryId = input.CategoryId,
            Images = input.Images?.Select(p => p.ToEntity()).ToList() ?? new List<Image>()
        };
    }

    public static void MapTo(this CreateUpdateProductDto dto, Product entity)
    {
        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.Price = dto.Price;
        entity.CategoryId = dto.CategoryId;
        if (dto.Images != null)
        {
            var imagesToRemove = entity.Images.Where(p => !dto.Images.Any(d => d.ImageUrl == p.ImageUrl)).ToList();
            foreach (var image in imagesToRemove)
            {
                entity.Images.Remove(image);
            }
            foreach (var imageDto in dto.Images)
            {
                var existingImage = entity.Images.FirstOrDefault(p => p.ImageUrl == imageDto.ImageUrl);
                if (existingImage == null)
                {
                    entity.Images.Add(imageDto.ToEntity());
                }
                else
                {
                    imageDto.MapTo(existingImage);
                }
            }
        }
    }

    public static Product UpdateEntity(this Product entity, CreateUpdateProductDto dto)
    {
        dto.MapTo(entity);
        return entity;
    }
}
