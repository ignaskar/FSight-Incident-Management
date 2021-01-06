using System.Security.Claims;

namespace FSight.Core.Interfaces
{
    public interface IUserAccessor
    {
        ClaimsPrincipal User { get; }
    }
}