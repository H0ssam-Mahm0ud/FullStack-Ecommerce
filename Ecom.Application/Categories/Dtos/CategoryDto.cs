using Ecom.Application.Dtos;

namespace Ecom.Application.Categories.Dtos;

public class CategoryDto : BaseDto<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
}
