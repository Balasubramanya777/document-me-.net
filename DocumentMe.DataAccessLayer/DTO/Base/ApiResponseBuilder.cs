using Base.DataAccessLayer.DTO.Base;
using System.Net;

namespace DocumentMe.DataAccessLayer.DTO.Base
{
    public class ApiResponseBuilder<T>
    {
        private T? _data;
        private bool _success;
        private string? _message;
        private HttpStatusCode _code;

        public ApiResponseBuilder<T> Data(T? data) { _data = data; return this; }
        public ApiResponseBuilder<T> Success(bool success) { _success = success; return this; }
        public ApiResponseBuilder<T> Message(string? message) { _message = message; return this; }
        public ApiResponseBuilder<T> Code(HttpStatusCode code) { _code = code; return this; }
        public ApiResponse<T> Build()
        {
            return new ApiResponse<T>(_data, _success, _message, _code);
        }
    }
}
