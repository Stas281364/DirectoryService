using CSharpFunctionalExtensions;
using TimeZoneConverter;

namespace DirectoryService.Domain.Locations;

public record TimeZone
{
    public string Value { get; }

    public TimeZone(string value)
    {
        Value = value;
    }

    public static Result<TimeZone> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<TimeZone>("Timezone cannot be empty");

        try
        {
            //TimeZoneInfo.FindSystemTimeZoneById(value);
            TZConvert.GetTimeZoneInfo(value);
        }
        catch
        {
            return Result.Failure<TimeZone>("Invalid timezone");
        }

        return new TimeZone(value);
    }
}