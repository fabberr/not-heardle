using System;

namespace NotHeardle.Domain.Common;

public sealed record class UtcDateTime
{
    public DateTime Value { get; init; }

    private UtcDateTime(DateTime value)
        => Value = value;

    public static Result<UtcDateTime> FromDateTime(DateTime dateTime)
    {
        if (dateTime.Kind is not DateTimeKind.Utc)
        {
            return Result.FromError(dateTime, "The value must be specified in Coordinated Universal Time (UTC).");
        }

        return new UtcDateTime(dateTime);
    }
}

