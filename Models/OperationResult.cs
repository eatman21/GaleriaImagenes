namespace GaleriaImagenes.Models
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Exception? Exception { get; set; }

        public OperationResult()
        {
            Success = false;
            Message = string.Empty;
        }

        public static OperationResult SuccessResult(string message = "Operation completed successfully")
        {
            return new OperationResult
            {
                Success = true,
                Message = message
            };
        }

        public static OperationResult FailureResult(string message, Exception? exception = null)
        {
            return new OperationResult
            {
                Success = false,
                Message = message,
                Exception = exception
            };
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T? Data { get; set; }

        public static OperationResult<T> SuccessResult(T data, string message = "Operation completed successfully")
        {
            return new OperationResult<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public new static OperationResult<T> FailureResult(string message, Exception? exception = null)
        {
            return new OperationResult<T>
            {
                Success = false,
                Message = message,
                Exception = exception
            };
        }
    }
}
