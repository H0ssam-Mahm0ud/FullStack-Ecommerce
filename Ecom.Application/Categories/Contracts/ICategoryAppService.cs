using Ecom.Application.Categories.Dtos;
using Ecom.Application.Results;
using Ecom.Application.Shared.Contracts;
using Ecom.Domain.Pagination;

namespace Ecom.Application.Categories.Contracts;

public interface ICategoryAppService : ICrudAppService
    <CreateUpdateCategoryDto,
    Result<CategoryDto>, 
    Guid,
    PagedAndSortedAndSearchResultRequestDto, 
    Result<PagedResultDto<CategoryDto>>>,
    IGetListAppService<Result<List<CategoryDto>>>
{
}
