using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.Position;

public class Position
{
    public Guid Id { get; set; }
    public Name NamePosition { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } //soft-delete
    public DateTime CreatedAt { get; set; } //utc
    public DateTime UpdatedAt { get; set; } //utc

    //EF core error
    private Position()
    {
    }

    private Position(Name name, string? description)
    {
        NamePosition = name;
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
        NamePosition = name;
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