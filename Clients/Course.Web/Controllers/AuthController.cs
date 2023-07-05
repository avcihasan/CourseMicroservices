using Course.Web.Models;
using Course.Web.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Course.Web.Controllers
{
    public class AuthController : Controller
    {
        readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVM signInVM)
        {
            if (!ModelState.IsValid)
                return View();

           var result = await _identityService.SignInasync(signInVM);

            if (!result.IsSuccessful)
            {
                result.Errors.ForEach(x =>ModelState.AddModelError(string.Empty, x));
                return View();
            }
            return RedirectToAction(nameof(HomeController.Index),"Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _identityService.LogoutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
