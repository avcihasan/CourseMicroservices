using Course.IdentityServer.Services.Abstractions;
using Course.Shared.CustomBaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace Course.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : CustomBaseController
    {
       readonly IUserService  _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
            =>CreateActionResult(await _userService.GetUserAsync(User.Claims.FirstOrDefault(x=>x.Type== JwtRegisteredClaimNames.Sub)));
    }
}
