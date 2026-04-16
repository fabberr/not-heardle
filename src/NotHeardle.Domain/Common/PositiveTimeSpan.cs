using System;

namespace NotHeardle.Domain.Common;

public sealed record class PositiveTimeSpan
{
    public TimeSpan Value { get; init; }

    public static PositiveTimeSpan Zero => new(TimeSpan.Zero);

    private PositiveTimeSpan(TimeSpan value)
        => Value = value;

    public static Result<PositiveTimeSpan> FromTimeSpan(TimeSpan timeSpan)
    {
        if (timeSpan < TimeSpan.Zero)
        {
            return Result.FromError(timeSpan, "Negative values are not allowed.");
        }

        return new PositiveTimeSpan(timeSpan);
    }

    public static Result<PositiveTimeSpan> FromSeconds(long seconds)
    {
        if (seconds < 0)
        {
            return Result.FromError(seconds, "Negative values are not allowed.");
        }

        return new PositiveTimeSpan(TimeSpan.FromSeconds(seconds));
    }

    public static Result<PositiveTimeSpan> FromSeconds(double seconds)
    {
        if (seconds < 0)
        {
            return Result.FromError(seconds, "Negative values are not allowed.");
        }

        return new PositiveTimeSpan(TimeSpan.FromSeconds(seconds));
    }

    public static Result<PositiveTimeSpan> FromTicks(long ticks)
    {
        if (ticks < 0)
        {
            return Result.FromError(ticks, "Negative values are not allowed.");
        }

        return new PositiveTimeSpan(TimeSpan.FromTicks(ticks));
    }
}
