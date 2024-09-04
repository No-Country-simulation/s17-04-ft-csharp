namespace JunioHub.Application.DTOs;

public class BaseResponse<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public List<string>? ValidationErrors { get; set; }

    public BaseResponse()
    {
        Success = true;
    }

    public BaseResponse(T? data)
    {
        Data = data;
    }

    public BaseResponse(string message)
    {
        Success = true;
        Message = message;
    }

    public BaseResponse(string message, bool success)
    {
        Success = success;
        Message = message;
    }

    public BaseResponse(T? data, bool success, string message, List<string>? validationErrors)
    {
        Data = data;
        Success = success;
        Message = message;
        ValidationErrors = validationErrors;
    }
}
