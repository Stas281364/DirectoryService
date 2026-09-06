using DirectoryService.Contracts;

namespace DirectoryService.Application;

public interface IDepartmentService
{
    public Task<Guid> Create(CreateDepartmentDto request, CancellationToken cancellationToken);
    
}