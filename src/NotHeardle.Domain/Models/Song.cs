using System;
using NotHeardle.Domain.Common;

namespace NotHeardle.Domain.Models;

#region Song Types
public sealed record class SongCore(
    PublicId SongId,
    NonEmptyString Name,
    PositiveTimeSpan Duration,
    AbsoluteUri StorageLocation
);

public abstract record class Song(SongCore Core)
{
    public Result<SongSlice> CreateRandomSlice(PositiveTimeSpan duration)
    {
        TimeSpan maxStart = Core.Duration.Value - duration.Value;

        double offsetSeconds = maxStart switch
        {
            { Ticks: <= 0 } => 0,
            _ => Random.Shared.NextDouble() * maxStart.TotalSeconds,
        };

        return PositiveTimeSpan.FromSeconds(offsetSeconds) switch
        {
            Success<PositiveTimeSpan> offsetSuccess
                => new SongSlice(this, offsetSuccess.Value, duration),

            Error<PositiveTimeSpan> offsetError
                => Result.FromError(offsetSeconds, offsetError.Message),

            _ => null!, // Unreachable
        };
    }
}

public sealed record class SoloTrack(
    SongCore Core,
    Artist Artist
) : Song(Core);

public sealed record class Collaboration(
    SongCore Core,
    Artist MainArtist,
    NonEmptyCollection<Artist> FeaturedArtists
) : Song(Core);
#endregion

public sealed record class Artist(
    PublicId ArtistId,
    NonEmptyString Name
);

public sealed record class SongSlice(
    Song Song,
    PositiveTimeSpan Offset,
    PositiveTimeSpan Duration
);
