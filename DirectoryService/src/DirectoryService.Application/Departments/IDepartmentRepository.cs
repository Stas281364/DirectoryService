using CSharpFunctionalExtensions;
using DirectoryService.Contracts;
using DirectoryService.Domain.Departments;

namespace DirectoryService.Application;

public interface IDepartmentRepository
{
    Task<Guid> AddAsyncDepartment(Department request, CancellationToken cancellationToken);
    
    Task<Result<Department>> GetByIdAsync(Guid? departmentId, CancellationToken cancellationToken);
}