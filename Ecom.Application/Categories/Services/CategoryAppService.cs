using Ecom.Application.Categories.Contracts;
using Ecom.Application.Categories.Dtos;
using Ecom.Application.Categories.Mappings;
using Ecom.Application.Shared.Services;
using Ecom.Domain.Contracts;
using Ecom.Domain.Entities.Product;
using Ecom.Domain.Pagination;

namespace Ecom.Application.Categories.Services;

public class CategoryAppService : CrudAppService
    <Category, 
    CreateUpdateCategoryDto, 
    CategoryDto, 
    Guid, 
    PagedAndSortedAndSearchResultRequestDto>,
    ICategoryAppService
{
    public CategoryAppService(ICategoryRepository categoryRepository) : base(categoryRepository)
    {
    }


    protected override CategoryDto ToDto(Category entity)
        => entity.ToDto();

    protected override Category ToEntity(CreateUpdateCategoryDto input)
        => input.ToEntity();

    protected override void UpdateEntity(CreateUpdateCategoryDto input, Category entity)
        => entity.UpdateEntity(input);
}
