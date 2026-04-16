using System;
using System.Runtime.CompilerServices;

namespace NotHeardle.Domain.Common;

public abstract record class Result<T>
{
    public static implicit operator Result<T>(T value) => new Success<T>(value);

    public static implicit operator Result<T>(Error error) => (Error<T>)error;
}

public sealed record class Success<T>(T Value) : Result<T>;

public sealed record class Error<T>(string FieldName, string Message) : Result<T>
{
    /// <summary>
    /// Error message format.
    /// </summary>
    /// <remarks>
    /// <c>"[{T}::{origin}] {message}"</c>
    /// </remarks>
    private const string DebugMessageFormat = "[{0}::{1}] {2}";

    public Func<string> DebugMessageFormatter { get; init; } = () => Message;

    public static implicit operator Error<T>(Error error)
    {
        var (fieldName, message, origin) = error;

        return new Error<T>(fieldName, message)
        {
            DebugMessageFormatter = () => string.Format(
                DebugMessageFormat,
                typeof(T).FullName, origin, message
            )
        };
    }
}

public sealed record class Error(string FieldName, string Message, string Origin);

public static class Result
{
    public static Error FromError<TField>(
#pragma warning disable IDE0060 // Remove unused parameter
        TField field,
#pragma warning restore IDE0060 // Remove unused parameter
        string message,
        [CallerArgumentExpression(nameof(field))] string fieldName = null!,
        [CallerMemberName] string origin = null!
    )
    {
        ArgumentException.ThrowIfNullOrEmpty(message);
        ArgumentException.ThrowIfNullOrEmpty(fieldName);
        ArgumentException.ThrowIfNullOrEmpty(origin);

        return new Error(fieldName, message, origin);
    }
}
