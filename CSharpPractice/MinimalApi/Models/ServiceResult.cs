namespace MinimalApi.Models;

public enum ServiceResultStatus
{
    Success,
    ValidationError,
    Conflict,
    NotFound
}

public sealed record ServiceResult<T>(ServiceResultStatus Status, T? Value, string? Error)
{
    public static ServiceResult<T> Success(T value) => new(ServiceResultStatus.Success, value, null);

    public static ServiceResult<T> ValidationError(string error) => new(ServiceResultStatus.ValidationError, default, error);

    public static ServiceResult<T> Conflict(string error) => new(ServiceResultStatus.Conflict, default, error);

    public static ServiceResult<T> NotFound(string error) => new(ServiceResultStatus.NotFound, default, error);
}
