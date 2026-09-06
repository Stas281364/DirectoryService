using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Contracts;
using DirectoryService.Domain.Departments;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Application.GetDepartment;

public class GetDepartmentHandler : ICommandHandler<Department, GetDepartmentCommand>
{

    private readonly ILogger<GetDepartmentHandler> _logger;
    private readonly IDepartmentRepository _repository;
    private readonly IValidator<GetDepartmentDto> _validator;
    
    public GetDepartmentHandler(
        IDepartmentRepository departmentRepository,
        ILogger<GetDepartmentHandler> logger, 
        IValidator<GetDepartmentDto> validator)
    {
        _repository = departmentRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Department>> Handle(GetDepartmentCommand command, CancellationToken cancellationToken)
    {
        //Создание валидности (Проверка входных данных / проверка данных бд)
        /*Валилация для Guid не нужна*/
        //var validationResult = _validator.ValidateAsync(command.departmentDto, cancellationToken);

        //Валидация бизнесс логики

        //Получение сущности через GET
        var result = await _repository.GetByIdAsync(command.departmentDto.Id, cancellationToken);
        
        if (result.IsFailure)
        {
            //Код
            return Result.Failure<Department>("Department not fount");
        }
        //Сохранение сущности Department в БД
        return result.Value;
        //Логгирование об успехе или отказной ситуации(ошибки сохранения)
    }
}