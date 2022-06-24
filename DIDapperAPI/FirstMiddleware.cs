using System.Net;
using DIDapperAPI.Model;

namespace DIDapperAPI
{
    public class FirstMiddleware
    {
        static HttpClient _http = new HttpClient();
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public FirstMiddleware(RequestDelegate next, ILogger<FirstMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //var url = context.Request.Path.ToUriComponent();
            //var uri = new Uri("https://localhost:7057" + url);
            //try
            //{
            //    var request = CopyRequest(context, uri);
            //    var remoteRsp = await _http.SendAsync(request);
            //    var rsp = context.Response;
            //    foreach (var header in remoteRsp.Headers)
            //    {
            //        rsp.Headers.Add(header.Key, header.Value.ToArray());
            //    }
            //    rsp.ContentType = remoteRsp.Content.Headers.ContentType.ToString();
            //    rsp.ContentLength = remoteRsp.Content.Headers.ContentLength;
            //    await remoteRsp.Content.CopyToAsync(rsp.Body);
            //}
            //catch (HttpRequestException e)
            //{

            //    Console.WriteLine(e.InnerException.Message);
            //}
            //Request
            string path = context.Request.Path.ToString();
            string querystring = context.Request.QueryString.ToString();
            string message = string.Format("path={0}, queryString={1}", path, querystring);

            try
            {
                _logger.LogWarning(message);
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware."
            }.ToString());
        }
        static HttpRequestMessage CopyRequest(HttpContext context, Uri targetUri)
        {
            var req = context.Request;
            var requestMessage = new HttpRequestMessage()
            {
                Method = new HttpMethod(req.Method),
                Content = new StreamContent(req.Body),
                RequestUri = targetUri,
            };

            foreach (var header in req.Headers)
            {
                requestMessage.Content.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            }

            requestMessage.Headers.Host = targetUri.Host;

            return requestMessage;
        }
    }
}
