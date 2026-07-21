using Ecom.Application.Products.Contracts;
using Ecom.Application.Products.Dtos;
using Ecom.Application.Products.Mappings;
using Ecom.Application.Results;
using Ecom.Application.Shared.Contracts;
using Ecom.Application.Shared.Services;
using Ecom.Domain.Constants;
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
    private readonly IFileService _fileService;
    private readonly IProductRepository _productRepository;
    public ProductAppService(IProductRepository productRepository, IFileService fileService)
        : base(productRepository)
    {
        _fileService = fileService;
        _productRepository = productRepository;
    }


    protected override ProductDto ToDto(Product entity)
        => entity.ToDto();

    protected override Product ToEntity(CreateUpdateProductDto input)

        => input.ToEntity();

    protected override void UpdateEntity(CreateUpdateProductDto input, Product entity)
        => entity.UpdateEntity(input);

    public override async Task<Result<ProductDto>> CreateAsync(CreateUpdateProductDto input)
    {
        if (input.Images?.Any() == true)
        {
            foreach (var image in input.Images)
            {
                if (image.ImageFile != null)
                {
                    image.ImageUrl = await _fileService.SaveFileAsync(image.ImageFile, RootFolders.Products);
                }
            }
        }

        return await base.CreateAsync(input);
    }

    public override async Task<Result<ProductDto>> UpdateAsync(CreateUpdateProductDto input, Guid id)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);

        if (existingProduct?.Images?.Any() == true && input.Images?.Any() == true)
        {
            foreach (var oldImage in existingProduct.Images)
            {
                _fileService.DeleteFile(oldImage.ImageUrl);
            }
        }

        if (input.Images?.Any() == true)
        {
            foreach (var image in input.Images)
            {
                if (image.ImageFile != null)
                {
                    image.ImageUrl = await _fileService.SaveFileAsync(image.ImageFile, RootFolders.Products);
                }
            }
        }

        return await base.UpdateAsync(input, id);
    }

    public override async Task<Result<ProductDto>> DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product?.Images?.Any() == true)
        {
            foreach (var image in product.Images)
            {
                _fileService.DeleteFile(image.ImageUrl);
            }
        }

        return await base.DeleteAsync(id);
    }
}
