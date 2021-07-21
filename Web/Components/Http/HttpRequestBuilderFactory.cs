using System.Net.Http;

namespace Web.Components.Http
{
    public class HttpRequestBuilderFactory : IHttpRequestBuilderFactory
    {
        private readonly HttpClient httpClient;

        public HttpRequestBuilderFactory(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("external-api");
        }

        public IHttpRequestBuilder Create(string uri)
        {
            return new HttpRequestBuilder(httpClient, uri);
        }
    }
}