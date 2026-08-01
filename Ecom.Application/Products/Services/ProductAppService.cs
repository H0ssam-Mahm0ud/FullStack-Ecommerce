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
        var product = ToEntity(input);

        product.Images ??= new List<Image>();

        if (input.NewImages?.Any() == true)
        {
            var savedUrls = await _fileService.SaveFileAsync(input.NewImages, RootFolders.Products);

            foreach (var url in savedUrls)
            {
                product.Images.Add(new Image { ImageUrl = url });
            }
        }

        await _productRepository.InsertAsync(product);
        return Result<ProductDto>.Success(ToDto(product));
    }

    public override async Task<Result<ProductDto>> UpdateAsync(CreateUpdateProductDto input, Guid id)
    {
        var existingProduct = await _baseRepository.GetByIdAsync(id);
        if (existingProduct == null)
        {
            return Result<ProductDto>.NotFound("Product not found");
        }

        existingProduct.Images ??= new List<Image>();

        if (existingProduct.Images.Any())
        {
            var urlsToKeep = input.ExistingImageUrls ?? new List<string>();
            var imagesToRemove = existingProduct.Images
                .Where(old => !urlsToKeep.Contains(old.ImageUrl))
                .ToList();

            foreach (var oldImage in imagesToRemove)
            {
                _fileService.DeleteFile(oldImage.ImageUrl);
                existingProduct.Images.Remove(oldImage); 
            }
        }

        if (input.NewImages?.Any() == true)
        {
            var savedUrls = await _fileService.SaveFileAsync(input.NewImages, RootFolders.Products);

            foreach (var url in savedUrls)
            {
                existingProduct.Images.Add(new Image { ImageUrl = url });
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