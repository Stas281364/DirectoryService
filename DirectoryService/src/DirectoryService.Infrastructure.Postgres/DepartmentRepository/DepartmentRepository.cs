using CSharpFunctionalExtensions;
using DirectoryService.Application;
using DirectoryService.Contracts;
using DirectoryService.Domain.Departments;

namespace DirectoryService.Infrastructure.Postgres.DepartmentRepository;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly DirectoryServiceDbContext _dbContext;

    public DepartmentRepository(DirectoryServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> AddAsyncDepartment(Department department, CancellationToken cancellationToken)
    {
        await _dbContext.Departments.AddAsync(department, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return department.Id.Value;
    }

    public async Task<Result<Department>> GetByIdAsync(Guid? departmentId, CancellationToken cancellationToken)
    {
        if (departmentId == Guid.Empty)
        {
            return null;
        }
        
        var depId = new DepartmentId(departmentId.Value);
        var Result = await _dbContext.Departments.FindAsync(depId, cancellationToken);
        return Result;
    }
}