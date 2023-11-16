namespace WebAPI.Utilities.Results;

public class SuccessResult : Result
{
    public SuccessResult(string message)
        : base(true, message)
    {
    }

    public SuccessResult()
        : base(true)
    {
    }
}

public class SuccessResult<T> : Result<T>
{
    public SuccessResult(T data, string message)
        : base(data, true, message)
    {
    }

    public SuccessResult(T data)
        : base(data, true)
    {
    }

    public SuccessResult(string message)
        : base(default!, true, message)
    {
    }

    public SuccessResult()
        : base(default!, true)
    {
    }
}