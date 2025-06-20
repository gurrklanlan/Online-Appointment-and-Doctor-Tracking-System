using System.Net;
using System.Text.Json.Serialization;

namespace App.Application
{
    public class ServiceResult<T>
    {
        public List<string>? ErrorMessages { get; set; }
        public T? Data { get; set; }
        [JsonIgnore]
        public string? UrlAsCreated { get; set; }
        [JsonIgnore] public bool IsSuccess => ErrorMessages == null || ErrorMessages.Count() == 0;
        [JsonIgnore] public bool IsFail => !IsSuccess;
        [JsonIgnore] public HttpStatusCode StatusCode { get; set; }

        public static ServiceResult<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ServiceResult<T>
            {
                Data = data,
                StatusCode = statusCode,
               
            };
        }

        public static ServiceResult<T> SuccessAsCreated(T data, string url)
        {
            return new ServiceResult<T>
            {
                Data = data,                
                UrlAsCreated = url
            };
        }

        public static ServiceResult<T> Fail(List<string> errorMessages, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                ErrorMessages = errorMessages,
                StatusCode = statusCode,
            };
        }

        public static ServiceResult<T> Fail(string errorMessages, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                ErrorMessages = [errorMessages],
                StatusCode = statusCode,
            };
        }

    }

    public class ServiceResult
    {
        public List<string>? ErrorMessages { get; set; }      
        [JsonIgnore]
        public string? UrlAsCreated { get; set; }
        [JsonIgnore] public bool IsSuccess => ErrorMessages == null || ErrorMessages.Count() == 0;
        [JsonIgnore] public bool IsFail => !IsSuccess;
        [JsonIgnore] public HttpStatusCode StatusCode { get; set; }

        public static ServiceResult Success( HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ServiceResult
            {
              
                StatusCode = statusCode,

            };
        }

        public static ServiceResult SuccessAsCreated( string url)
        {
            return new ServiceResult
            {
               
                UrlAsCreated = url
            };
        }

        public static ServiceResult Fail(List<string> errorMessages, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult
            {
                ErrorMessages = errorMessages,
                StatusCode = statusCode,
            };
        }

        public static ServiceResult Fail(string errorMessages, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult
            {
                ErrorMessages = [errorMessages],
                StatusCode = statusCode,
            };
        }

    }



}
