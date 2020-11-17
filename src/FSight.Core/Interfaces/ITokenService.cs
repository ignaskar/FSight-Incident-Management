using System.Threading.Tasks;
using FSight.Core.Entities.Identity;

namespace FSight.Core.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}