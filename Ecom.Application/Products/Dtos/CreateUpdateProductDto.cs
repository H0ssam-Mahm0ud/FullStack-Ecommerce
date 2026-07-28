using Ecom.Application.Photos.Dtos;
using Microsoft.AspNetCore.Http;

namespace Ecom.Application.Products.Dtos;

public class CreateUpdateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public List<IFormFile>? NewImages { get; set; }
    public List<string>? ExistingImageUrls { get; set; }
}
