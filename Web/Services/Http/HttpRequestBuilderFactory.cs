using System.Net.Http;

namespace Web.Services.Http
{
    public class HttpRequestBuilderFactory : IHttpRequestBuilderFactory
    {
        private readonly HttpClient httpClient;

        public HttpRequestBuilderFactory(IHttpClientFactory httpClient)
        {
            this.httpClient = httpClient.CreateClient("api");
        }

        public IHttpRequestBuilder Create(string uri)
        {
            return new HttpRequestBuilder(httpClient, uri);
        }
    }
}