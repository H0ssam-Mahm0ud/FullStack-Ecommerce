using Ecom.Application.Dtos;

namespace Ecom.Application.Photos.Dtos;

public class ImageDto : BaseDto<Guid>
{
    public string ImageUrl { get; set; }
}
