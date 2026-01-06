using DocumentMe.DataAccessLayer.DTO.Base;
using System.Net;

namespace Base.DataAccessLayer.DTO.Base
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public T? Data { get; set; }

        public ApiResponse(T? data, bool success, string? message, HttpStatusCode code)
        {
            Success = success;
            Message = message;
            Data = data;
            StatusCode = code;
        }

        public static ApiResponseBuilder<T> Builder() => new ApiResponseBuilder<T>(); 
        public static ApiResponse<T> SuccessResponse(T? data, bool success, string? message, HttpStatusCode code) => new(data, success, message, code);

    }
}
