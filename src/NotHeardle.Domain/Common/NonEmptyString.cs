using NotHeardle.Domain.Common;

namespace NotHeardle.Domain.Models;

public sealed record class NonEmptyString
{
    public string Value { get; init; }

    private NonEmptyString(string name)
        => Value = name;

    public static Result<NonEmptyString> FromString(string @string)
    {
        if (string.IsNullOrWhiteSpace(@string))
        {
            return Result.FromError(@string, "String cannot be empty.");
        }

        return new NonEmptyString(@string);
    }
}
