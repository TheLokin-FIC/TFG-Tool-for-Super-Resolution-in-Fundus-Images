using System;
using System.Threading.Tasks;

namespace Web.Services.Http
{
    public interface IHttpRequestBuilder
    {
        Task Post<T>(T data);

        Task Get(object data = null);

        Task Put<T>(T data);

        Task Delete();

        IHttpRequestBuilder OnOk(Action todo);

        IHttpRequestBuilder OnOk<T>(Action<T> todo);

        IHttpRequestBuilder OnCreated(Action todo);

        IHttpRequestBuilder OnCreated<T>(Action<T> todo);

        IHttpRequestBuilder OnBadRequest(Action todo);

        IHttpRequestBuilder OnBadRequest<T>(Action<T> todo);

        IHttpRequestBuilder OnNotFound(Action todo);

        IHttpRequestBuilder OnNotFound<T>(Action<T> todo);

        IHttpRequestBuilder OnInternalServerError(Action todo);

        IHttpRequestBuilder OnInternalServerError<T>(Action<T> todo);
    }
}