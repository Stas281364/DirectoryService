using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.Departments;

public record Identifier
{
    private const UInt16 MIN_LENGHT = 3;
    private const UInt16 MAX_LENGTH = 150;
    private string Value { get; }

    public Identifier()
    {
        
    }

    public Identifier(string value)
    {
        Value = value;
    }

    public static Result<Identifier> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)
            || value.Length < MIN_LENGHT || value.Length > MAX_LENGTH
            || Regex.IsMatch(value, @"^[a-zA-Z0-9 ]+$"))
        {
            return Result.Failure<Identifier>(
                $"The Identifier {value} is invalid: is null or less 3 or more 150 or is`n latin");
        }

        return new Identifier(value);
    }
}