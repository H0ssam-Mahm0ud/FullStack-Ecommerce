using Ecom.Application.Categories.Contracts;
using Ecom.Application.Categories.Services;
using Ecom.Application.Products.Contracts;
using Ecom.Application.Products.Services;
using Ecom.Application.Shared.Contracts;
using Ecom.Application.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Ecom.Application.Registerations;

public static class ServicesRegisteration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICategoryAppService, CategoryAppService>();
        services.AddScoped<IProductAppService, ProductAppService>();
        services.AddScoped<IFileService, FileService>();
        

        return services;
    }
}
