using System;
using NotHeardle.Domain.Common;

public sealed record class PublicId
{
    public Guid Value { get; init; }

    private PublicId(Guid value)
        => Value = value;
    
    public static Result<PublicId> FromGuid(Guid guid)
    {
        if (guid == Guid.Empty)
        {
            return Result.FromError(guid, "The value cannot be empty.");
        }

        return new PublicId(guid);
    }
}
