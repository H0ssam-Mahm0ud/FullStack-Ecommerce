using Ecom.Application.Photos.Dtos;
using Ecom.Domain.Entities.Product;

namespace Ecom.Application.Photos.Mappings;

public static class ImageMappings
{
    public static ImageDto ToDto(this Image input)
    {
        return new ImageDto
        {
            Id = input.Id,
            ImageUrl = input.ImageUrl
        };
    }

    public static Image ToEntity(this CreateUpdateImageDto input)
    {
        return new Image
        {
            ImageUrl = input.ImageUrl
        };
    }

    public static void MapTo(this CreateUpdateImageDto dto, Image entity)
    {
        entity.ImageUrl = dto.ImageUrl;
    }
}
