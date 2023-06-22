using Course.IdentityServer.DTOs;
using Course.IdentityServer.Models;
using Course.Shared.CustomBaseController;
using Course.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace Course.IdentityServer.Controllers
{
    
    public class AuthController : CustomBaseController
    {
        readonly UserManager<ApplicationUser>   _userManager;

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
        {
            ApplicationUser user = new ApplicationUser() { Email= signUpDto.Email,UserName=signUpDto.Email };
            IdentityResult result =await _userManager.CreateAsync(user, signUpDto.Password);
            if (!result.Succeeded)
                return CreateActionResult(ResponseDto<NoContentDto>.Fail(result.Errors.Select(x => x.Description).ToList(), HttpStatusCode.BadRequest));

            return CreateActionResult(ResponseDto<NoContentDto>.Success(HttpStatusCode.NoContent));
        }
    }
}
