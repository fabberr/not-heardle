using System;

namespace NotHeardle.Domain.Common;

public sealed record class AbsoluteUri
{
    public Uri Value { get; init; }

    private AbsoluteUri(Uri value)
        => Value = value;

    public static Result<AbsoluteUri> FromUri(Uri uri)
    {
        if (uri.IsAbsoluteUri is false)
        {
            return Result.FromError(uri, "The value must be an absolute URI.");
        }

        return new AbsoluteUri(uri);
    }

    public static Result<AbsoluteUri> FromString(string uriString)
    {
        try
        {
            var uri = new Uri(uriString, UriKind.Absolute);

            return new AbsoluteUri(uri);
        }
        catch (Exception ex)
        {
            return Result.FromError(uriString, ex.Message);
        }
    }
}
