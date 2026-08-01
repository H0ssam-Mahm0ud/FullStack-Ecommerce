using Ecom.Application.Registerations;
using Ecom.Infrastructure.Registerations;
using Microsoft.Extensions.FileProviders;

namespace Ecom.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        //builder.Services.AddOpenApi();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.RegisterDbContext(builder.Configuration);
        builder.Services.RegisterRepositories(builder.Configuration);
        builder.Services.RegisterServices(builder.Configuration);
        builder.Services.AddScoped<IFileProvider>(sp =>
        {
            var env = sp.GetRequiredService<IWebHostEnvironment>();
            var webRoot = !string.IsNullOrEmpty(env.WebRootPath)
                ? env.WebRootPath
                : Path.Combine(env.ContentRootPath, "wwwroot");

            var uploadPath = Path.Combine(webRoot, "Images");
            Directory.CreateDirectory(uploadPath); // safe even if exists
            return new PhysicalFileProvider(uploadPath);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            //app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
