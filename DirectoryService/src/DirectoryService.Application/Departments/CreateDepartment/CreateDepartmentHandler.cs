using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
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
    
    public CreateDepartmentHandler(ILogger<CreateDepartmentHandler> logger,
        IDepartmentRepository repository,
        IValidator<CreateDepartmentDto> validator)
    {
        _logger = logger;
        _repository = repository;
        _validator = validator;
    }
    
    public async Task<Result<Guid>> Handle(CreateDepartmentCommand command, CancellationToken cancellationToken)
    {
        //Создание валидности (Проверка входных данных / проверка данных бд)
        var validationResult = _validator.ValidateAsync(command.departmentDto, cancellationToken);

        //Валидация бизнесс логики

        //Создание сущности
        
        ////Надо Name
        var departmentNameResult = Name.Create(command.departmentDto.Name);
        ////Надо identifier(slug)
        var departmentSlugResult = Identifier.Create(command.departmentDto.Identifier);
        ////Надо ParentId
        var departmentParent = await _repository.GetByIdAsync(command.departmentDto.IdTopDepartment, cancellationToken);
        ////Надо Path
        //var departmentPath = Path.Create(command.departmentDto.Path);
        
        ////Depth нужен не сейчас, он появится в DS-21 - сейчас NULL или 0
        short depth = 0;
        //Разветвление на то, если есть родитель ИЛИ это корень

        //Резальтат запроса по созданию Department
        Result<Department> result;
        
        //Если departmentParent нет, то это корень и создавать Department c null DepartmentParent 
        if (departmentParent.Value == null)
        {
            var departmentPath = Path.Create(departmentSlugResult.Value.Value);
            
            result = Department.Create(departmentNameResult.Value,
                departmentSlugResult.Value,
                null,
                departmentPath.Value,
                depth,
                command.departmentDto.LocationIds);
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