using DirectoryService.Application.Abstractions;
using DirectoryService.Contracts;

namespace DirectoryService.Application;

//(команда, токен)
public record GetDepartmentCommand (GetDepartmentDto departmentDto, CancellationToken CancellationToken) : ICommands;