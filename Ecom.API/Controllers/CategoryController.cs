using Ecom.Application.Categories.Contracts;
using Ecom.Application.Categories.Dtos;
using Ecom.Application.Results;
using Ecom.Domain.Filters;
using Ecom.Domain.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryAppService _categoryAppService;

    public CategoryController(ICategoryAppService categoryAppService)
    {
        _categoryAppService = categoryAppService;
    }



    /// <summary>
    /// Retrieves all categories without pagination.
    /// </summary>
    [HttpGet("all")]
    public async Task<Result<List<CategoryDto>>> GetAllAsync()
    {
        return await _categoryAppService.GetAllAsync();
    }

    /// <summary>
    /// Retrieves categories with optional search, filtering, sorting, and pagination.
    /// </summary>
    [HttpGet]
    public async Task<Result<PagedResultDto<CategoryDto>>> GetAllPaginatedAsync(
        [FromQuery] PagedAndSortedAndSearchResultRequestDto pagination)
        //,[FromQuery] List<FilterRequestDto>? filters)
    {

        return await _categoryAppService.GetAllPaginatedAsync(pagination);
        //return await _categoryAppService.GetAllPaginatedAsync(pagination, filters);
    }

    /// <summary>
    /// Gets a specific category by its unique identifier.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<Result<CategoryDto>> GetByIdAsync(Guid id)
    {
        return await _categoryAppService.GetByIdAsync(id);
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    [HttpPost]
    public async Task<Result<CategoryDto>> CreateAsync([FromBody] CreateUpdateCategoryDto input)
    {
        return await _categoryAppService.CreateAsync(input);
    }

    /// <summary>
    /// Updates an existing category by its identifier.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<Result<CategoryDto>> UpdateAsync([FromBody] CreateUpdateCategoryDto input, Guid id)
    {
        return await _categoryAppService.UpdateAsync(input, id);
    }

    /// <summary>
    /// Deletes a specific category.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<Result<CategoryDto>> DeleteAsync(Guid id)
    {
        return await _categoryAppService.DeleteAsync(id);
    }
}
