using System.Collections.Generic;
using System.Linq;
using NotHeardle.Domain.Common;

public sealed record class NonEmptyCollection<T>
{
    public List<T> Values { get; init; }

    private NonEmptyCollection(List<T> values)
        => Values = values;
    
    public static Result<NonEmptyCollection<T>> FromEnumerable(IEnumerable<T> values)
    {
        if (!values.Any())
        {
            return Result.FromError(values, "Collection cannot be empty.");
        }

        return new NonEmptyCollection<T>([.. values]);
    }

    public static Result<NonEmptyCollection<T>> FromCollection(ICollection<T> values)
    {
        if (values.Count is 0)
        {
            return Result.FromError(values, "Collection cannot be empty.");
        }

        return new NonEmptyCollection<T>([.. values]);
    }

    public static Result<NonEmptyCollection<T>> FromList(List<T> values)
    {
        if (values.Count is 0)
        {
            return Result.FromError(values, "Collection cannot be empty.");
        }

        return new NonEmptyCollection<T>(values);
    }
}
