using CSharpFunctionalExtensions;
using DirectoryService.Domain.Department;

namespace DirectoryService.Domain.Position;

public class Position
{
    private List<DepartmentPosition.DepartmentPosition> _departmentPositions = [];
    public PositionId Id { get; private set; }
    public Name PositionName { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; } //soft-delete
    public DateTime CreatedAt { get; private set; } //utc
    public DateTime UpdatedAt { get; private set; } //utc
    public IReadOnlyCollection<DepartmentPosition.DepartmentPosition> DepartmentPositions => _departmentPositions;

    //EF core error
    private Position()
    {
    }

    private Position(Name positionName, string? description)
    {
        Id = new PositionId(Guid.NewGuid());
        PositionName = positionName;
        Description = description;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public static Result<Position> Create(Name name, string? description)
    {
        if (string.IsNullOrWhiteSpace(description) || description.Length > 1000)
        {
            return Result.Failure<Position>("description is invalid");
        }

        return new Position(name, description);
    }

    public Result ChangeName(Name name)
    {
        PositionName = name;
        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }
    
    public Result ChangeDescription(string description)
    {
        Description = description;
        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }
    
    public Result Delete()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }
}