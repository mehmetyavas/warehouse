namespace WebAPI.Utilities.Results;

public class ErrorResult : Result
{
    public ErrorResult(string message)
        : base(false, message)
    {
    }

    public ErrorResult()
        : base(false)
    {
    }
}

public class ErrorResult<T> : Result<T>
{
    public ErrorResult(T data, string message)
        : base(data, false, message)
    {
    }

    public ErrorResult(T data)
        : base(data, false)
    {
    }

    public ErrorResult(string message)
        : base(default, false, message)
    {
    }

    public ErrorResult()
        : base(default, false)
    {
    }
}