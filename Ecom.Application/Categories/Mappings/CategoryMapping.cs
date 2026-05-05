using Ecom.Application.Categories.Dtos;
using Ecom.Domain.Entities.Product;

namespace Ecom.Application.Categories.Mappings;

public static class CategoryMapping
{
    public static CategoryDto ToDto(this Category input)
    {
        return new CategoryDto
        {
            Name = input.Name,
            Description = input.Description,
            //Products = input.Products.Select(e => e.ToDto()).ToList()
        };
    }


    public static Category ToEntity(this CreateUpdateCategoryDto input)
    {
        return new Category
        {
            Name = input.Name,
            Description = input.Description,
        };
    }


    public static void MapTo(this CreateUpdateCategoryDto dto, Category entity)
    {
        entity.Name = dto.Name;
        entity.Description = dto.Description;
    }

    public static Category UpdateEntity(this Category entity, CreateUpdateCategoryDto dto)
    {
        if (entity == null || dto == null) 
            return entity;

        entity.Name = dto.Name;
        entity.Description = dto.Description;
        return entity;
    }
}
