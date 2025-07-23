using System.Net;

namespace MinervaFoods.Helpers.Http
{
    public class HttpRequestException : System.Exception
    {
        public HttpStatusCode StatusCode { get; }
        public HttpResponseMessage? HttpResponseMessage { get; }


        public HttpRequestException(HttpStatusCode statusCode)
            : base($"API request failed with status code {statusCode}")
        {
            StatusCode = statusCode;
            HttpResponseMessage = new HttpResponseMessage(statusCode);
        }

        public HttpRequestException(HttpStatusCode statusCode, string message, System.Exception? innerException = null)
         : base(message ?? $"API request failed with status code {statusCode}", innerException)
        {
            StatusCode = statusCode;
        }

        public HttpRequestException(string message, System.Exception? innerException = null)
            : base(message, innerException)
        {
            message = message ?? throw new ArgumentNullException("response");
        }
    }
}
