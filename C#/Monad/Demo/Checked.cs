abstract record Checked<T, TProblem>(T Value)
{
    public static Checked<T, TProblem> Unit(T value) => new Passed<T, TProblem>(value);
    public static Checked<T, TProblem> Failed(T value, TProblem problem) => new Failed<T, TProblem>(value, problem);

    public abstract Checked<T, TProblem> Bind(Func<T, Checked<T, TProblem>> func);
    public abstract Checked<U, TProblem> Bind<U>(Func<T, Checked<U, TProblem>> func, U orElse);

    public abstract Checked<U, TProblem> Map<U>(Func<T, U> func);

    public abstract U Match<U>(Func<T, U> onPassed, Func<T, TProblem, U> onFailed);
}

record Passed<T, TProblem>(T Value) : Checked<T, TProblem>(Value)
{
    public override Checked<T, TProblem> Bind(Func<T, Checked<T, TProblem>> func) =>
        func(Value);

    public override Checked<U, TProblem> Bind<U>(Func<T, Checked<U, TProblem>> func, U orElse) =>
        func(Value);

    public override Checked<U, TProblem> Map<U>(Func<T, U> func) =>
        new Passed<U, TProblem>(func(Value));

    public override U Match<U>(Func<T, U> onPassed, Func<T, TProblem, U> onFailed) =>
        onPassed(Value);
}

record Failed<T, TProblem>(T Value, TProblem Problem) : Checked<T, TProblem>(Value)
{
    public override Checked<T, TProblem> Bind(Func<T, Checked<T, TProblem>> func) =>
        this;

    public override Checked<U, TProblem> Bind<U>(Func<T, Checked<U, TProblem>> func, U orElse) =>
        new Failed<U, TProblem>(orElse, Problem);

    public override Checked<U, TProblem> Map<U>(Func<T, U> func) =>
        new Failed<U, TProblem>(default!, Problem);

    public override U Match<U>(Func<T, U> onPassed, Func<T, TProblem, U> onFailed) =>
        onFailed(Value, Problem);
}