using System.Net;

namespace D_APP_API.Middlewares
{
    public class ApiException : Exception
    {
        private HttpStatusCode _httpStatusCode;
        public HttpStatusCode StatusCode { get => _httpStatusCode; }

        public ApiException() : base() { }

        public ApiException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message)
        {
            _httpStatusCode = statusCode;
        }
    }

}
