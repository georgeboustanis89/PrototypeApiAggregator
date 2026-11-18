namespace PrototypeApiAggregator.Models
{
    public class ResponseResult<T> where T : class
    {
        public bool Success { get; init; }
        public string? Error { get; init; }
        public T? Data { get; init; }
        public int? StatusCode { get; init; }

        private ResponseResult() { }

        public static ResponseResult<T> Ok(T data, int? statusCode = null)
        {
            return new()
            {
                Success = true,
                Data = data,
                Error = null,
                StatusCode = statusCode
            };
        }

        public static ResponseResult<T> Fail(string error, int? statusCode = null)
        {
            return new()
            {
                Success = false,
                Data = null,
                Error = error,
                StatusCode = statusCode
            };
        }

    }
}
