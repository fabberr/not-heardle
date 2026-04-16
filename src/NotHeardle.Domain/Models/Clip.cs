namespace NotHeardle.Domain.Models;

public sealed record class Clip(
    PublicId ClipId,
    SongSlice Slice
);
