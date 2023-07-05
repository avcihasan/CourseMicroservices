using Course.Shared.DTOs;
using Course.Web.Models;
using IdentityModel.Client;

namespace Course.Web.Services.Abstractions
{
    public interface IIdentityService
    {
        Task<ResponseDto<bool>> SignInasync(SignInVM signIn );
        Task LogoutAsync();
        Task<TokenResponse> GetAccessTokenByRefreshTokenAsync();
        Task RevokeRefreshTokenAsync();
    }
}
