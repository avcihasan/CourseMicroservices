using Course.Web.Exceptions;
using Course.Web.Models;
using Course.Web.Services.Abstractions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Course.Web.Controllers
{
    public class HomeController : Controller
    {
        readonly ICatalogService _catalogService;

        public HomeController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
            => View(await _catalogService.GetAllCoursesAsync());
        public async Task<IActionResult> Detail(string id)
            => View(await _catalogService.GetCourseByIdAsync(id));

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            IExceptionHandlerFeature errorFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (errorFeature != null && errorFeature.Error is UnAuthorizeException)
                return RedirectToAction(nameof(AuthController.Logout), "Auth");

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}