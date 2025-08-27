namespace VerticalSlicesArchitecture.ErrorResponse;

public record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public static readonly Error Null = new(ErrorCodes.Null, "The specified result value is null.");

    public static readonly Error ConditionNotMet = new(ErrorCodes.ConditionNotMet, "The specified condition was not met.");
}