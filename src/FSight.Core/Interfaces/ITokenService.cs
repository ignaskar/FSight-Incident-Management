using FSight.Core.Entities.Identity;

namespace FSight.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}