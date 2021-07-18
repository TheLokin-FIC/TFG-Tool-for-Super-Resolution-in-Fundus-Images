using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web.Components.Http
{
    public class HttpRequestBuilder : IHttpRequestBuilder
    {
        private readonly IDictionary<HttpStatusCode, Func<HttpResponseMessage, Task>> responses;
        private readonly HttpClient httpClient;
        private readonly string uri;

        public HttpRequestBuilder(HttpClient httpClient, string uri)
        {
            responses = new Dictionary<HttpStatusCode, Func<HttpResponseMessage, Task>>();
            this.httpClient = httpClient;
            this.uri = uri;
        }

        public async Task Post<T>(T data)
        {
            await HandleHttpResponse(async () =>
            {
                return await httpClient.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json"));
            });
        }

        public async Task Get()
        {
            await HandleHttpResponse(async () =>
            {
                return await httpClient.GetAsync(uri);
            });
        }

        public async Task Put<T>(T data)
        {
            await HandleHttpResponse(async () =>
            {
                return await httpClient.PutAsync(uri, new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json"));
            });
        }

        public async Task Delete()
        {
            await HandleHttpResponse(async () =>
            {
                return await httpClient.DeleteAsync(uri);
            });
        }

        public IHttpRequestBuilder OnOk(Action todo)
        {
            responses.Add(HttpStatusCode.OK, OnResponse(todo));

            return this;
        }

        public IHttpRequestBuilder OnOk<T>(Action<T> todo)
        {
            responses.Add(HttpStatusCode.OK, OnResponse(todo));

            return this;
        }

        public IHttpRequestBuilder OnCreated(Action todo)
        {
            responses.Add(HttpStatusCode.Created, OnResponse(todo));

            return this;
        }

        public IHttpRequestBuilder OnCreated<T>(Action<T> todo)
        {
            responses.Add(HttpStatusCode.Created, OnResponse(todo));

            return this;
        }

        public IHttpRequestBuilder OnBadRequest(Action todo)
        {
            responses.Add(HttpStatusCode.BadRequest, OnResponse(todo));

            return this;
        }

        public IHttpRequestBuilder OnBadRequest<T>(Action<T> todo)
        {
            responses.Add(HttpStatusCode.BadRequest, OnResponse(todo));

            return this;
        }

        public IHttpRequestBuilder OnNotFound(Action todo)
        {
            responses.Add(HttpStatusCode.NotFound, OnResponse(todo));

            return this;
        }

        public IHttpRequestBuilder OnNotFound<T>(Action<T> todo)
        {
            responses.Add(HttpStatusCode.NotFound, OnResponse(todo));

            return this;
        }

        public IHttpRequestBuilder OnInternalServerError(Action todo)
        {
            responses.Add(HttpStatusCode.InternalServerError, OnResponse(todo));

            return this;
        }

        public IHttpRequestBuilder OnInternalServerError<T>(Action<T> todo)
        {
            responses.Add(HttpStatusCode.InternalServerError, OnResponse(todo));

            return this;
        }

        private async Task HandleHttpResponse(Func<Task<HttpResponseMessage>> httpCall)
        {
            HttpResponseMessage response = await httpCall();
            if (responses.TryGetValue(response.StatusCode, out Func<HttpResponseMessage, Task> onResponse))
            {
                await onResponse(response);
            }
        }

        private static Func<HttpResponseMessage, Task> OnResponse(Action todo)
        {
            return (HttpResponseMessage response) =>
            {
                todo();

                return Task.CompletedTask;
            };
        }

        private static Func<HttpResponseMessage, Task> OnResponse<T>(Action<T> todo)
        {
            return async (HttpResponseMessage response) =>
            {
                todo(JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()));
            };
        }
    }
}