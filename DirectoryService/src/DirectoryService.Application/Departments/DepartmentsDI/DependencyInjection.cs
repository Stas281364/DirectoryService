using DirectoryService.Application.Abstractions;
using DirectoryService.Application.Location;
using DirectoryService.Application.Locations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DirectoryService.Application.DepartmentsDI;

public static class DependencyInjection
{
    public static IServiceCollection AddDepartmentService(this IServiceCollection services)
    {
        //Добавление(регистрация) всех сервисов в dependencyInjection
        
        //Регистрация валидатора
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        //Сервис локации
        //services.AddScoped<ICommandHandler<Guid, CreateDepartmentCommand>, CreateDepartmentHandler>();
        
        
        return services;
    }
}