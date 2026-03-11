using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.Location;

public record TimeZone
{
    public string Value { get; }

    private TimeZone(string value)
    {
        Value = value;
    }

    public static Result<TimeZone> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<TimeZone>("Timezone cannot be empty");

        try
        {
            TimeZoneInfo.FindSystemTimeZoneById(value);
        }
        catch
        {
            return Result.Failure<TimeZone>("Invalid timezone");
        }

        return new TimeZone(value);
    }
}