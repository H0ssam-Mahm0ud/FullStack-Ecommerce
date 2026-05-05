using Ecom.Domain.Contracts;
using Ecom.Domain.Entities.Product;
using Ecom.Infrastructure.Context;
using Ecom.Infrastructure.Repositories.Generic;

namespace Ecom.Infrastructure.Repositories;

public class ImageRepository : GenericRepository<Image, Guid>, IImageRepository
{
    public ImageRepository(AppDbContext appDbContext) : base(appDbContext)
    { 
    }
}
