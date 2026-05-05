using Ecom.Domain.Contracts.Generic;
using Ecom.Domain.Entities.Product;

namespace Ecom.Domain.Contracts;

public interface ICategoryRepository : IGenericRepository<Category, Guid>
{
}
