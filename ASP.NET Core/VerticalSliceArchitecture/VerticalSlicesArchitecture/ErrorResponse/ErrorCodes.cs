namespace VerticalSlicesArchitecture.ErrorResponse;

public static class ErrorCodes
{
    public const string Null = "Error.Null";
    public const string ConditionNotMet = "Error.ConditionNotMet";

    public static class User
    {
        public static class Create
        {
            public const string Validation = "CreateUser.Validation";
        }

        public static class Update
        {
            public const string Validation = "UpdateUser.Validation";
        }

        public static class Get
        {
            public const string NotFound = "GetUser.NotFound";
        }
    }
}