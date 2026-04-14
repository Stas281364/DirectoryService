using DirectoryService.Application.Location;
using DirectoryService.Application.Locations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DirectoryService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddLocationService(this IServiceCollection services)
    {
        //Добавление(регистрация) всех сервисов в dependencyInjection
        
        //Регистрация валидатора
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        //Сервис локации
        services.AddScoped<ILocationService, LocationService>();
        
        
        return services;
    }
}