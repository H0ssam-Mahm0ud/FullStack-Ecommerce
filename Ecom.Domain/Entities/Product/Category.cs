using Ecom.Domain.Entities;

namespace Ecom.Domain.Entities.Product;

public class Category : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
}
