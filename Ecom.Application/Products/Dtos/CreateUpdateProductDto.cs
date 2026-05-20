using Ecom.Domain.Entities.Product;

namespace Ecom.Application.Products.Dtos;

public class CreateUpdateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public virtual List<CreateUpdatePhotoDto>? Photos { get; set; } = new();
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }
}
