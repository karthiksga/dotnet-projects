namespace JwtAndRefreshTokens.Features.Shared;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public T? Data { get; private set; }
    public List<ClientAuthorizationError>? Errors { get; private set; }

    private Result(bool isSuccess, T? data, List<ClientAuthorizationError>? errors)
    {
        IsSuccess = isSuccess;
        Data = data;
        Errors = errors;
    }

    public static Result<T> Success(T data) =>
        new(true, data, null);

    public static Result<T> Failure(List<ClientAuthorizationError> errors) =>
        new(false, default, errors);

    public static Result<T> Failure(string code, string message) =>
        new(false, default, [new ClientAuthorizationError(code, message)]);
}

public class ClientAuthorizationError(string code, string message)
{
    public string Code { get; set; } = code;
    public string Message { get; set; } = message;
}
