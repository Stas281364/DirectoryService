using System.Diagnostics.CodeAnalysis;
using DirectoryService.Domain.Departments;
using DirectoryService.Domain.Locations;

namespace DirectoryService.Domain.DepartmentLocations;

public sealed class DepartmentLocation
{
    /*public Guid LocationId { get; init; }
    public Guid DepartmentId { get; init; }*/
    
    public Guid Id { get; init; }
    public required LocationId LocationId { get; init; }
    public required DepartmentId DepartmentId { get; init; }
    
    // ✅ EF Core будет использовать этот конструктор
    private DepartmentLocation() { }

    [SetsRequiredMembers]
    public DepartmentLocation(Guid locationId, Guid departmentId)
    {
        LocationId = new LocationId(locationId);
        DepartmentId = new DepartmentId(departmentId);
    }
}