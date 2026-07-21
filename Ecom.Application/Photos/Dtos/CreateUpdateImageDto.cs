using Ecom.Domain.Entities.Product;
using Microsoft.AspNetCore.Http;

namespace Ecom.Application.Photos.Dtos;

public class CreateUpdateImageDto
{
    public IFormFile? ImageFile { get; set; }
    public string? ImageUrl { get; set; }
}
