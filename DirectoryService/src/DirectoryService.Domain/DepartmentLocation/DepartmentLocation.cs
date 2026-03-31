using DirectoryService.Domain.Department;
using DirectoryService.Domain.Location;

namespace DirectoryService.Domain.DepartmentLocation;

public sealed class DepartmentLocation
{
    public Guid Id { get; init; }
    public required LocationId LocationId { get; init; }
    public required DepartmentId DepartmentId { get; init; }
}