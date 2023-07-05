using Course.IdentityServer.DTOs;
using Course.IdentityServer.Models;
using Course.IdentityServer.Services.Abstractions;
using Course.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Course.IdentityServer.Services.Concretes
{
    public class UserService:IUserService
    {
        readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResponseDto<UserDto>> GetUserAsync(Claim userClaim)
        {
            if (userClaim !=null)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(userClaim.Value);
                if (user != null)
                {
                    return ResponseDto<UserDto>.Success(new UserDto(){Id=user.Id,Email=user.Email, PhoneNumber = user.PhoneNumber,UserName=user.UserName },HttpStatusCode.OK);
                }
            }
            return ResponseDto<UserDto>.Fail(string.Empty,HttpStatusCode.BadRequest);
        }
    }
}
