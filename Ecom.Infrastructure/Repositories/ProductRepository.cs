using Ecom.Domain.Contracts;
using Ecom.Domain.Entities.Product;
using Ecom.Infrastructure.Context;
using Ecom.Infrastructure.Repositories.Generic;

namespace Ecom.Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product, Guid>, IProductRepository
{
    public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
