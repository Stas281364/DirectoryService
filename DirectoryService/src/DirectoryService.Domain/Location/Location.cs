namespace DirectoryService.Domain.Location;

public class Location
{
    public Guid Id { get; set; }
    public required string Adress { get; set; }
    public Guid? DepartmentId { get; set; }
}