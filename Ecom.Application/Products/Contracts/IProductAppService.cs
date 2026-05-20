using Ecom.Application.Categories.Dtos;
using Ecom.Application.Products.Dtos;
using Ecom.Application.Results;
using Ecom.Application.Shared.Contracts;
using Ecom.Domain.Pagination;

namespace Ecom.Application.Products.Contracts;

public interface IProductAppService : ICrudAppService
    <CreateUpdateProductDto,
    Result<ProductDto>,
    Guid,
    PagedAndSortedAndSearchResultRequestDto,
    Result<PagedResultDto<ProductDto>>>,
    IGetListAppService<Result<List<CategoryDto>>>
{
}
