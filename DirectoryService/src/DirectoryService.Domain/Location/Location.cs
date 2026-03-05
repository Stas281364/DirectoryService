using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.Location;

public class Location
{
    public Guid Id { get; private set; }
    public Name LocationName { get; private set; } //ValueObject
    public Address Address { get; private set; } //в бд может быть несколько столбцов или jsonb
    public TimeZone TimeZone { get; private set; } //IANA-код
    public bool IsActive { get; private set; } //soft-delete
    public DateTime CreatedAt { get; private set; } //utc
    public DateTime UpdatedAt { get; private set; } //utc

    //EF error
    private Location()
    {
    }

    private Location(Name locationName, Address address, TimeZone timeZone)
    {
        Id = Guid.NewGuid();
        LocationName = locationName;
        Address = address;
        TimeZone = timeZone;
        IsActive = true;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    //Создане модели
    public static Result<Location> Create(Name locationName, Address address, TimeZone timeZone)
    {
        return new Location(locationName, address, timeZone);
    }

    public Result ChangeLocationName(Name locationName)
    {
        LocationName = locationName;
        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }

    public Result ChangeAddress(Address address)
    {
        Address = address;
        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }

    public Result ChangeTimeZone(TimeZone timeZone)
    {
        TimeZone = timeZone;
        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }

    public Result Delete()
    {
        IsActive = false;
        UpdatedAt = DateTime.Now;
        return Result.Success();
    }
    
}