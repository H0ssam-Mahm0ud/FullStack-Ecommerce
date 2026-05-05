using Ecom.Domain.Contracts;
using Ecom.Domain.Entities.Product;
using Ecom.Infrastructure.Context;
using Ecom.Infrastructure.Repositories.Generic;

namespace Ecom.Infrastructure.Repositories;

public class CategoryRepository : GenericRepository<Category, Guid>, ICategoryRepository
{
    public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
