using Course.IdentityServer.DTOs;
using Course.Shared.DTOs;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Course.IdentityServer.Services.Abstractions
{
    public interface IUserService
    {
        Task<ResponseDto<UserDto>> GetUserAsync(Claim userClaim);
    }
}
