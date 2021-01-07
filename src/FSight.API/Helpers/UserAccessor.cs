using System.Security.Claims;
using FSight.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FSight.API.Helpers
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _accessor;

        public UserAccessor(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public ClaimsPrincipal User => _accessor.HttpContext.User;
    }
}