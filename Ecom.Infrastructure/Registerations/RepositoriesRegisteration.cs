using Ecom.Domain.Contracts;
using Ecom.Domain.Contracts.Generic;
using Ecom.Infrastructure.Repositories;
using Ecom.Infrastructure.Repositories.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecom.Infrastructure.Registerations;

/// <summary>
/// Provides methods for registering mapping.
/// </summary>
public static class RepositoriesRegisteration
{
    /// <summary>
    /// Registers the AutoMapper services with the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection RegisterRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        // register the generic repository
        services.AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));


        // all the repositories should be registered here
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IImageRepository, ImageRepository>();



        return services;
    }
}
