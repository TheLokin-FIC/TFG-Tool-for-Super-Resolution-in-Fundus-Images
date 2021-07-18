namespace Web.Components.Http
{
    internal interface IHttpRequestBuilderFactory
    {
        IHttpRequestBuilder Create(string uri);
    }
}