using RequestProcessor.App.Models;
using RequestProcessor.App.Models.Impl;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RequestProcessor.App.Services.Impl
{
    internal class RequestHandler : IRequestHandler
    {
        private HttpClient _client;
        public RequestHandler(HttpClient client)
        {
            _client = client;
        }

        public async Task<IResponse> HandleRequestAsync(IRequestOptions requestOptions)
        {
            Console.WriteLine("Request started...");
            if (requestOptions == null) throw new ArgumentNullException(nameof(requestOptions));
            if (!requestOptions.IsValid) throw new ArgumentOutOfRangeException(nameof(requestOptions));
            using var msg = new HttpRequestMessage(MapMethod(requestOptions.Method),new Uri(requestOptions.Address));
            if (MapMethod(requestOptions.Method) != HttpMethod.Get)
            {
                msg.Content = new StringContent(requestOptions.Body, Encoding.UTF8, requestOptions.ContentType);
                using var responseForPushingData = await _client.SendAsync(msg);
                var bodyForPushing = await responseForPushingData.Content.ReadAsStringAsync();
                return new Response(true, (int)responseForPushingData.StatusCode, "Pushed:\n" + bodyForPushing);
            }
                using var response = await _client.SendAsync(msg);
                var body = await response.Content.ReadAsStringAsync();
                return new Response(true, (int)response.StatusCode, body);
        }
        
        private static HttpMethod MapMethod(RequestMethod method)
        {
            switch (method)
            {

                case RequestMethod.Get:
                    return HttpMethod.Get;
                case RequestMethod.Post:
                    return HttpMethod.Post;
                case RequestMethod.Put:
                    return HttpMethod.Put;
                case RequestMethod.Patch:
                    return HttpMethod.Patch;
                case RequestMethod.Delete:
                    return HttpMethod.Delete;
                default:
                    throw new ArgumentOutOfRangeException(nameof(method), method, "Invalid request method");
            }
        }
    }
}
