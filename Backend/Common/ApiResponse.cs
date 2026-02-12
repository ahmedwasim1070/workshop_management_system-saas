namespace Backend.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }

    public static ApiResponse<T> Ok(T? Data, string? Message = null)
    {
        return new ApiResponse<T> { Success = true, Data = Data, Message = Message ?? "Successful" };
    }

    public static ApiResponse<T> Fail(string Message)
    {
        return new ApiResponse<T> { Success = false, Data = default, Message = Message };
    }
}