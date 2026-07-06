using Ecom.Application.Products.Contracts;
using Ecom.Application.Products.Dtos;
using Ecom.Application.Results;
using Ecom.Domain.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductAppService _productAppService;

    public ProductController(IProductAppService productAppService)
    {
        _productAppService = productAppService;
    }

    /// <summary>
    /// Retrieves all products without pagination.
    /// </summary>
    [HttpGet("all")]
    public async Task<Result<List<ProductDto>>> GetAllAsync()
    {
        return await _productAppService.GetAllAsync();
    }

    /// <summary>
    /// Retrieves products with optional search, filtering, sorting, and pagination.
    /// </summary>
    [HttpGet("All-Paginated")]
    public async Task<Result<PagedResultDto<ProductDto>>> GetAllPaginatedAsync(
        [FromQuery] PagedAndSortedAndSearchResultRequestDto pagination)
    {
        return await _productAppService.GetAllPaginatedAsync(pagination);
    }

    /// <summary>
    /// Gets a specific product by its unique identifier.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<Result<ProductDto>> GetByIdAsync(Guid id)
    {
        return await _productAppService.GetByIdAsync(id);
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    [HttpPost]
    public async Task<Result<ProductDto>> CreateAsync([FromBody] CreateUpdateProductDto input)
    {
        return await _productAppService.CreateAsync(input);
    }

    /// <summary>
    /// Updates an existing product by its identifier.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<Result<ProductDto>> UpdateAsync([FromBody] CreateUpdateProductDto input, Guid id)
    {
        return await _productAppService.UpdateAsync(input, id);
    }

    /// <summary>
    /// Deletes a specific product.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<Result<ProductDto>> DeleteAsync(Guid id)
    {
        return await _productAppService.DeleteAsync(id);
    }
}
