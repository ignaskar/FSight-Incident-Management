using System.Net.Http;
using FSight.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FSight.API.Helpers
{
    public class HttpResponseAccessor : IHttpResponseAccessor
    {
        private readonly IHttpContextAccessor _accessor;

        public HttpResponseAccessor(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public HttpResponse Response => _accessor.HttpContext.Response;
    }
}