namespace DirectoryService.Domain.DepartmentPosition;

public class DepartmentPosition
{
    public Guid Id { get; set; }
    public required List<Guid> DepartmentIds { get; set; }
    public required List<Guid> PositionIds { get; set; }
}