using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace Course.Shared.DTOs
{
    public class ResponseDto<T>
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        [JsonIgnore]
        public bool IsSuccessful { get; set; }

        public static ResponseDto<T> Success(T data, HttpStatusCode statusCode)
            => new ResponseDto<T>() { Data = data, StatusCode = (int)statusCode,IsSuccessful=true };
        public static ResponseDto<T> Success(HttpStatusCode statusCode)
            => new ResponseDto<T>() { Data = default(T), StatusCode = (int)statusCode, IsSuccessful = true };
        public static ResponseDto<T> Fail(List<string> errors,HttpStatusCode statusCode)
            => new ResponseDto<T>() { Errors=errors, StatusCode = (int)statusCode, IsSuccessful = false };
        public static ResponseDto<T> Fail(string error, HttpStatusCode statusCode)
            => new ResponseDto<T>() { Errors = new List<string>() { error }, StatusCode = (int)statusCode, IsSuccessful = false };
    }
}
