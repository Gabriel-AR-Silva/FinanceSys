public class Result<T>
{
    public bool Success { get; set;}
    public T? Data { get; set; }
    public string? Error { get; set; }

    public Result<T> OK(T data)
    {
       return new Result<T>
       {
            Success = true,
            Data = data,
            Error = null
       };
    }

    public Result<T> Fail(string error)
    {
        return new Result<T>
        {
            Success = false,
            Data = default,
            Error = error  
        };
    }
}