using Ecom.Application.Categories.Contracts;
using Ecom.Application.Categories.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecom.Application.Registerations;

public static class ServicesRegisteration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICategoryAppService, CategoryAppService>();

        return services;
    }
}
