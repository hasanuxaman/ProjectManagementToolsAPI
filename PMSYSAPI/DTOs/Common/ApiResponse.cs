public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    // ==================== HELPER METHODS ====================
    public static ApiResponse<T> Ok(string message, T? data = default)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    public static ApiResponse<T> Fail(string message, List<string>? errors = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors
        };
    }

    public static ApiResponse<T> Error(string message, Exception ex)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = new List<string> { ex.Message }
        };
    }
}

// Non-generic version for endpoints that don't return data
public class ApiResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string>? Errors { get; set; }

    public static ApiResponse Ok(string message)
    {
        return new ApiResponse { Success = true, Message = message };
    }

    public static ApiResponse Fail(string message, List<string>? errors = null)
    {
        return new ApiResponse { Success = false, Message = message, Errors = errors };
    }

    public static ApiResponse Error(string message, Exception ex)
    {
        return new ApiResponse { Success = false, Message = message, Errors = new List<string> { ex.Message } };
    }
}
