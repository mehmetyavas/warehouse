namespace WebAPI.Utilities.Results;

public class Result : IResult
{
    public Result(bool success, string message)
        : this(success)
    {
        Message = message;
    }

    public Result(bool success)
    {
        Success = success;
    }

    public bool Success { get; set; }
    public string Message { get; set; }
}

public class Result<T> : Result, IResult<T>
{
    public Result(T data, bool success, string message)
        : base(success, message)
    {
        Data = data;
    }

    public Result(T data, bool success)
        : base(success)
    {
        Data = data;
    }

    public T Data { get; set; }
}