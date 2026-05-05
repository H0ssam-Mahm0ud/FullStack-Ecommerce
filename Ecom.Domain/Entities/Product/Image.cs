using System.ComponentModel.DataAnnotations.Schema;
using Ecom.Domain.Entities;

namespace Ecom.Domain.Entities.Product;

public class Image : BaseEntity<Guid>
{
    public string ImageName { get; set; }


    //foreign keys
    public Guid ProductId { get; set; }
    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }
}
