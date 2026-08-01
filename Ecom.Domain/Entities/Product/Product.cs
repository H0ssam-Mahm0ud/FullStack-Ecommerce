using System.ComponentModel.DataAnnotations.Schema;
using Ecom.Domain.Entities;

namespace Ecom.Domain.Entities.Product;

public class Product : AuditableEntity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public virtual List<Image>? Images { get; set; }

    // foreign keys
    public Guid CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }
}
