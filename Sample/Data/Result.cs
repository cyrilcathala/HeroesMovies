using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Sample.Data
{
    public interface IResult
    {
        string Code { get; }
        string Message { get; }
        bool IsSuccess { get; }
        bool IsFailure { get; }
    }

    public interface IResult<T>: IResult
    {
        T Content { get; }
    }

    public class Result: IResult
    {
        public string Code { get; protected set; }

        public string Message { get; protected set; }

        public Exception Exception { get; protected set; }

        public bool IsSuccess { get; protected set; } = true;

        public bool IsFailure
        {
            get => !IsSuccess;
            set => IsSuccess = !value;
        }

        protected Result(bool isSuccess = true, string code = null, string message = null)
        {
            IsSuccess = isSuccess;
            Code = code;
            Message = message;

            if (Code == null)
                Code = IsSuccess ? "OK" : "ERROR";
        }

        protected Result(Exception exception = null, string code = null, string message = null)
            : this(exception == null, code, message)
        {
            Exception = exception;
        }

        public static IResult Success() => new Result(isSuccess: true);

        public static IResult<T> Success<T>(T content = default) => new Result<T>(content, true);

        public static IResult Failure(string code, string message = null) => new Result(false, code, message);

        public static IResult<T> Failure<T>(string code, string message = null) => new Result<T>(default, false, code, message);
    }

    public class Result<T>: Result, IResult<T>
    {
        public T Content { get; set; }

        public Result(T content, bool isSuccess = true, string code = null, string message = null)
            : base(isSuccess, code, message)
        {
            Content = content;
        }

        public Result(T content, Exception exception = null, string code = null, string message = null)
            : base(exception, code, message)
        {
            Content = content;
        }
    }

    public class HttpResult<T>: Result<T>
    {
        public HttpResponseMessage HttpResponse { get; set; }

        public bool IsTimeout => IsTimeoutStatus(HttpResponse?.StatusCode) || Exception is TimeoutException || Exception is WebException;
        public bool IsCanceled => Exception is OperationCanceledException;

        public HttpResult(
            HttpResponseMessage httpResponseMessage = null,
            Exception exception = null,
            T content = default,
            string code = null,
            string message = null)
            : base(content, false, code, message)
        {
            HttpResponse = httpResponseMessage;
            Exception = exception;
            IsSuccess = Exception == null && (httpResponseMessage?.IsSuccessStatusCode ?? true);

            if (HttpResponse != null && string.IsNullOrEmpty(code))
                Code = HttpResponse.StatusCode.ToString();
            if (string.IsNullOrEmpty(message))
            {
                if (HttpResponse != null)
                    Message = HttpResponse.ReasonPhrase;
                else if (Exception != null)
                    Message = Exception.Message;
            }
        }

        private bool IsTimeoutStatus(HttpStatusCode? httpCode) => httpCode != null
                                                        || httpCode == HttpStatusCode.RequestTimeout
                                                        || httpCode == HttpStatusCode.GatewayTimeout;

    }
}
