namespace Web.Services.Http
{
    internal interface IHttpRequestBuilderFactory
    {
        IHttpRequestBuilder Create(string uri);
    }
}