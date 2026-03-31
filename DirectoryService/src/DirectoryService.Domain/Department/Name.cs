using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.Department;

//ValueObject для Name
public record Name
{
    private const ushort MIN_LENGHT = 3;
    private const ushort MAX_LENGTH = 150;
    public string Value { get; }

    private Name()
    {
        
    }

    public Name(string value)
    {
        Value = value;
    }

    public static Result<Name> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < MIN_LENGHT || name.Length > MAX_LENGTH)
        {
            return Result.Failure<Name>($"The name {name} is invalid: is null or less 3 or more 150");
        }

        return new Name(name);
    }
}