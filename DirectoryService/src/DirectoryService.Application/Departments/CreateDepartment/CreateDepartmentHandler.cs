using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Application.Locations;
using DirectoryService.Contracts;
using DirectoryService.Domain.Departments;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Path = DirectoryService.Domain.Departments.Path;

namespace DirectoryService.Application.Departments.CreateDepartment;

public class CreateDepartmentHandler : ICommandHandler<Guid, CreateDepartmentCommand>
{
    private readonly ILogger<CreateDepartmentHandler> _logger;
    private readonly IDepartmentRepository _repository;
    private readonly IValidator<CreateDepartmentDto> _validator;
    private readonly ILocationRepository _locationRepository;
    
    public CreateDepartmentHandler(ILogger<CreateDepartmentHandler> logger,
        IDepartmentRepository repository,
        IValidator<CreateDepartmentDto> validator,
        ILocationRepository locationRepository)
    {
        _logger = logger;
        _repository = repository;
        _validator = validator;
        _locationRepository = locationRepository;
    }
    
    public async Task<Result<Guid>> Handle(CreateDepartmentCommand command, CancellationToken cancellationToken)
    {
        //Создание валидности (Проверка входных данных / проверка данных бд)
        var validationResult = await _validator.ValidateAsync(command.departmentDto, cancellationToken);

        //Валидация бизнесс логики
        if (!validationResult.IsValid)
            return Result.Failure<Guid>(validationResult.Errors.First().ErrorMessage);
        //Создание сущности
        
        ////Надо Name
        var departmentNameResult = Name.Create(command.departmentDto.Name);
        if (departmentNameResult.IsFailure)
            return Result.Failure<Guid>(departmentNameResult.Error);
        ////Надо identifier(slug)
        var departmentSlugResult = Identifier.Create(command.departmentDto.Identifier);
        if (departmentSlugResult.IsFailure)
            return Result.Failure<Guid>(departmentSlugResult.Error);
        ////Надо ParentId
        var departmentParent = await _repository.GetByIdAsync(command.departmentDto.IdTopDepartment, cancellationToken);
        
        //Исключение при введеннии ID в запрос, но отсутствии такого ID в базе
        if (command.departmentDto.IdTopDepartment != null && departmentParent.Value == null)
            return Result.Failure<Guid>("Указанный родительский департамент не найден");
        
        ////Надо Path
        //var departmentPath = Path.Create(command.departmentDto.Path);
        
        ////Depth нужен не сейчас, он появится в DS-21 - сейчас NULL или 0
        short depth = 0;
        //Разветвление на то, если есть родитель ИЛИ это корень

        //Резальтат запроса по созданию Department
        Result<Department> result;
        
        //Проверка на "Несуществующие идентификаторы" 
        var locationIds = command.departmentDto.LocationIds;
        if (locationIds.Any())
        {
            var existingLocations = await _locationRepository.GetAllExistingIdLocationAsync(locationIds, cancellationToken);
            var missingLocation = locationIds.Except(existingLocations);

            if (missingLocation.Any())
            {
                return Result.Failure<Guid>("Обнаружены несуществующие Id при созданиии Department"); 
            }
        }

        //Если departmentParent нет, то это корень и создавать Department c null DepartmentParent 
        if (departmentParent.Value == null)
        {
            var departmentPath = Path.Create(departmentSlugResult.Value.Value);
            
            result = Department.Create(departmentNameResult.Value,
                departmentSlugResult.Value,
                null,
                departmentPath.Value,
                depth,
                command.departmentDto.LocationIds); //Тут исправить - сделать проверку на "несуществующие идентификаторы"
            //надо сравнить id из базы данных со списком LocationIds
        }
        else
        {
            string fullPath = $"{departmentParent.Value.Path.Value}/{departmentSlugResult.Value.Value}";
            var departmentPath = Path.Create(fullPath);
            
            result = Department.Create(departmentNameResult.Value,
                departmentSlugResult.Value,
                departmentParent.Value,
                departmentPath.Value,
                depth,
                command.departmentDto.LocationIds);
        }
        
        if (result.IsFailure)
        {
            //Код
            
        }
        
        //Сохранение сущности Department в БД
        await _repository.AddAsyncDepartment(result.Value, cancellationToken);        
        //Логгирование об успехе или отказной ситуации(ошибки сохранения)
        _logger.LogInformation($"Department created with id {result.Value.Id}", result.Value.Id);

        return result.Value.Id.Value;
    }
}