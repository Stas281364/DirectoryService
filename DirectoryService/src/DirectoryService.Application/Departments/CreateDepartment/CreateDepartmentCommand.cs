using DirectoryService.Application.Abstractions;
using DirectoryService.Contracts;

namespace DirectoryService.Application;
//Заполни CreateDepHandler + Validator // Потом переходи к repository

//(команда, токен)
public record CreateDepartmentCommand(CreateDepartmentDto departmentDto, CancellationToken cancellationToken) : ICommands;