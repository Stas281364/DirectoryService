using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.Location;

public record Name
{
    private const ushort MIN_LENGHT = 3;
    private const ushort MAX_LENGTH = 120;
    private string Value { get; }

    private Name(string value)
    {
        Value = value;
    }

    public static Result<Name> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < MIN_LENGHT || name.Length > MAX_LENGTH)
        {
            return Result.Failure<Name>($"The name {name} is invalid: is null or less 3 or more 120");
        }

        return new Name(name);
    }
}