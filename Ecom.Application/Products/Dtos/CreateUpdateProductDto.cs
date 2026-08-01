using Ecom.Application.Photos.Dtos;
using Microsoft.AspNetCore.Http;

namespace Ecom.Application.Products.Dtos;

public class CreateUpdateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public Guid CategoryId { get; set; }
    public IFormFileCollection? NewImages { get; set; }
    public List<string>? ExistingImageUrls { get; set; }
}
