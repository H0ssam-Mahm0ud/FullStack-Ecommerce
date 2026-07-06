using Ecom.Application.Products.Contracts;
using Ecom.Application.Products.Dtos;
using Ecom.Application.Products.Mappings;
using Ecom.Application.Shared.Services;
using Ecom.Domain.Contracts;
using Ecom.Domain.Entities.Product;
using Ecom.Domain.Pagination;

namespace Ecom.Application.Products.Services;

public class ProductAppService : CrudAppService<
    Product,
    CreateUpdateProductDto,
    ProductDto,
    Guid,
    PagedAndSortedAndSearchResultRequestDto>,
    IProductAppService
{
    public ProductAppService(IProductRepository productRepository) : base(productRepository)
    {
    }


    protected override ProductDto ToDto(Product entity)
        => entity.ToDto();

    protected override Product ToEntity(CreateUpdateProductDto input)

        => input.ToEntity();

    protected override void UpdateEntity(CreateUpdateProductDto input, Product entity)
        => entity.UpdateEntity(input);
}
