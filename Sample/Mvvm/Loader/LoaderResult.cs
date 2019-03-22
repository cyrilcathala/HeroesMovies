using Sample.Mvvm;
using Sample.Data;

namespace Sample.Toolkit.Mvvm
{
    public class LoaderResult : ILoaderResult
    {
        public bool IsEmpty { get; private set; }
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }
        public string EmptyMessage { get; private set; }

        private LoaderResult()
        { }

        public static LoaderResult Success()
        {
            return new LoaderResult
            {
                IsSuccess = true
            };
        }

        public static LoaderResult Error(string errorMessage = "")
        {
            return new LoaderResult
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }

        public static LoaderResult Empty(string emptyMessage = "")
        {
            return new LoaderResult
            {
                IsSuccess = true,
                IsEmpty = true,
                EmptyMessage = emptyMessage
            };
        }

        public static LoaderResult FromResult(IResult result)
        {
            return new LoaderResult
            {
                IsSuccess = result.IsSuccess,
                ErrorMessage = result.Message,
            };
        }

        public static LoaderResult FromServiceResult<T>(HttpResult<T> httpResult)
        {
            var errorMessage = "";

            if (httpResult.IsFailure)
            {
                if (httpResult.IsTimeout)
                    errorMessage = "Please check your internet connection";
            }

            return new LoaderResult
            {
                IsSuccess = httpResult.IsSuccess,
                ErrorMessage = errorMessage,
                IsEmpty = httpResult.Content == null
            };
        }
    }
}
