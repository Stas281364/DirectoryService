namespace DirectoryService.Domain.DepartmentLocation;

public class DepartmentLocation
{
    public Guid Id { get; set; }
    public required List<Guid> DepartmentIds { get; set; }
    public required List<Guid> LocationIds { get; set; }
}