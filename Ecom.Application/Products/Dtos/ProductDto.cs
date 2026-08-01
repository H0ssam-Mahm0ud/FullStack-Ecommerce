using Ecom.Application.Dtos;
using Ecom.Application.Photos.Dtos;

namespace Ecom.Application.Products.Dtos;

public class ProductDto : BaseDto<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public ICollection<ImageDto> Images { get; set; } = new List<ImageDto>();
    public Guid CategoryId { get; set; }
}
