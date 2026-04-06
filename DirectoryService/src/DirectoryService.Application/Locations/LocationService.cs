using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using DirectoryService.Application.Locations;
using DirectoryService.Contracts;
using DirectoryService.Contracts.Location;
using DirectoryService.Domain.Locations;
using FluentValidation;
using Microsoft.Extensions.Logging;
using DirectoryService.Application.Locations;

namespace DirectoryService.Application.Location;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly ILogger<LocationService> _logger;
    private readonly IValidator<CreateLocationDto> _validator;

    public LocationService(ILocationRepository locationRepository,
        IValidator<CreateLocationDto> validator,
        ILogger<LocationService> logger)
    {
        _locationRepository = locationRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Guid> Create(CreateLocationDto locationDto, CancellationToken cancellationToken)
    {
        //Создание валидности(Проверка входных данных / проверка данных бд)
        var validationResult = await _validator.ValidateAsync(locationDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        //Валидация бизнесс логики

        //Создание сущности 
        var locationNameResult = Name.Create(locationDto.Name);
        var locationAddressResult = Address.Create(locationDto.Address);
        var locationTimeZoneResult = Domain.Locations.TimeZone.Create(locationDto.TimeZone);

        var result = Domain.Locations.Location.Create(locationNameResult.GetValueOrDefault(),
            locationAddressResult.Value,
            locationTimeZoneResult.Value);
        
        if (result.IsFailure)
        {
            ///код
        }

        //Сохранение сущности Department в БД
        await _locationRepository.AddAsync(result.Value, cancellationToken);
        
        //Логгирование об успехе или отказной ситуации(ошибки сохранения)
        _logger.LogInformation($"Location created with id {result.Value.Id}", result.Value.Id);

        return result.Value.Id.value;
    }

    public async Task UpdateById(Guid id, UpdateLocationDto locationDto, CancellationToken cancellationToken)
    {
         
    }
}