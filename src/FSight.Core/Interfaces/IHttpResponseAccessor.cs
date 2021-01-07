using Microsoft.AspNetCore.Http;

namespace FSight.Core.Interfaces
{
    public interface IHttpResponseAccessor
    {
        HttpResponse Response { get; }
    }
}