using Microsoft.AspNetCore.Http;

namespace Ecom.Application.Products.Dtos;

public class CreateUpdateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public List<IFormFile>? Images { get; set; } = new();
    public Guid CategoryId { get; set; }
}
