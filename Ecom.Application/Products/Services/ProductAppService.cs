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
        var entity = ToEntity(input);

        if (input.NewImages?.Any() == true)
        {
            foreach (var file in input.NewImages)
            {
                var savedUrl = await _fileService.SaveFileAsync(file, RootFolders.Products);
                entity.Images.Add(new Image { ImageUrl = savedUrl });
            }
        }

        bool success = await _baseRepository.InsertAsync(entity);
        if (!success) 
            return Result<ProductDto>.Error("Failed to create product.");

        return Result<ProductDto>.Success(ToDto(entity));
    }

    public override async Task<Result<ProductDto>> UpdateAsync(CreateUpdateProductDto input, Guid id)
    {
        var existingProduct = await _baseRepository.GetByIdAsync(id);
        if (existingProduct == null) return Result<ProductDto>.NotFound("Product not found");

        if (existingProduct.Images?.Any() == true)
        {
            var urlsToKeep = input.ExistingImageUrls ?? new List<string>();
            var imagesToRemove = existingProduct.Images
                .Where(old => !urlsToKeep.Contains(old.ImageUrl))
                .ToList();

            foreach (var oldImage in imagesToRemove)
            {
                _fileService.DeleteFile(oldImage.ImageUrl);
            }
        }

        if (input.NewImages?.Any() == true)
        {
            foreach (var file in input.NewImages)
            {
                var savedUrl = await _fileService.SaveFileAsync(file, RootFolders.Products);
                existingProduct.Images.Add(new Image { ImageUrl = savedUrl });
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
