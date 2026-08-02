using System.ComponentModel.DataAnnotations.Schema;
using Ecom.Domain.Entities;

namespace Ecom.Domain.Entities.Product;

public class Image : BaseEntity<Guid>
{
    public string ImageUrl { get; set; }


    //foreign keys
    public Guid ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
}
