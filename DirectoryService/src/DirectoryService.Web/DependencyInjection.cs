using DirectoryService.Application.Location;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DirectoryService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddProgramDependencies(this IServiceCollection services)
    {

        services.AddWebDependencies();
        //Общий регистрационный файл

        services.AddLocationService();
        return services;
    }

    private static IServiceCollection AddWebDependencies(this IServiceCollection services)
    {
        // Add services to the container.

        services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();

        return services;
    }
}