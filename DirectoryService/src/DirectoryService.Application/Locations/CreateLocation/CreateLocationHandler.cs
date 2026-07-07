using System.Windows.Input;
using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Application.Location;
using DirectoryService.Contracts.Location;
using FluentValidation;
using Microsoft.Extensions.Logging;
using DirectoryService.Domain.Locations;
using TimeZone = DirectoryService.Domain.Locations.TimeZone;
using DirectoryService.Application.Abstractions;

namespace DirectoryService.Application.Locations;

public class CreateLocationHandler : ICommandHanlder<Guid, CreateLocationCommand>
{
    private readonly ILogger<LocationService> _logger;
    private readonly ILocationRepository _locationRepository;
    private readonly IValidator<CreateLocationDto> _validator;
    
    
    
    public CreateLocationHandler(
        ILocationRepository locationRepository,
        ILogger<LocationService> logger, 
        IValidator<CreateLocationDto> validator)
    {
        _locationRepository = locationRepository;
        _logger = logger;
        _validator = validator;
    }
    
    public async Task<Result<Guid>> Handle(CreateLocationCommand command, CancellationToken cancellationToken)
    {
        //Создание валидности(Проверка входных данных / проверка данных бд)
        var validationResult = await _validator.ValidateAsync(command.locationDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        //Валидация бизнесс логики

        //Создание сущности 
        var locationNameResult = Name.Create(command.locationDto.Name);
        var locationAddressResult = Address.Create(command.locationDto.Address);
        var locationTimeZoneResult = TimeZone.Create(command.locationDto.TimeZone);
        
        var result = Domain.Locations.Location.Create(
            locationNameResult.GetValueOrDefault(),
            locationAddressResult.Value,
            locationTimeZoneResult.Value);
        
        if (result.IsFailure)
        {
            //Код
        }

        //Сохранение сущности Department в БД
        //await _locationRepository.AddAsync(result.Value, cancellationToken);
        await _locationRepository.AddAsync(result.Value, cancellationToken);
        
        //Логгирование об успехе или отказной ситуации(ошибки сохранения)
        _logger.LogInformation($"Location created with id {result.Value.Id}", result.Value.Id);

        return result.Value.Id.value;
    }

    
}