using DirectoryService.Domain.Department;
using DirectoryService.Domain.Position;

namespace DirectoryService.Domain.DepartmentPosition;

public class DepartmentPosition
{
    public Guid Id { get; init; }
    public required DepartmentId DepartmentId { get; init; }
    public required PositionId PositionId { get; init; }
}