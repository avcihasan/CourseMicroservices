using Course.Shared.Services.Abstractions;
using Course.Web.Models.CatalogModels;
using Course.Web.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Course.Web.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        readonly ICatalogService _catalogService;
        readonly ISharedIdentityService _sharedService;

        public CoursesController(ICatalogService catalogService, ISharedIdentityService sharedService)
        {
            _catalogService = catalogService;
            _sharedService = sharedService;
        }

        public async Task<IActionResult> Index()
            => View(await _catalogService.GetAllCoursesByUserIdAsync(_sharedService.UserId));

        public async Task<IActionResult> Create()
        {
            ViewBag.categoryList = new SelectList(await _catalogService.GetAllCategoriesAsync(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseVM createCourseVM)
        {
            ViewBag.categoryList = new SelectList(await _catalogService.GetAllCategoriesAsync(), "Id", "Name");
            if (!ModelState.IsValid)
                return View();

            createCourseVM.UserId = _sharedService.UserId;
            await _catalogService.CreateCourseAsync(createCourseVM);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(string id)
        {
            CourseVM course = await _catalogService.GetCourseByIdAsync(id);
            if (course is null)
                return RedirectToAction(nameof(Index));
            ViewBag.categoryList = new SelectList(await _catalogService.GetAllCategoriesAsync(), "Id", "Name", course.Id);
            UpdateCourseVM updateCourse = new()
            {
                Id = course.Id,
                Description = course.Description,
                Name = course.Name,
                Feature = course.Feature,
                Picture = course.Picture,
                Price = course.Price,
                UserId = course.UserId,
                CategoryId = course.CategoryId
            };
            return View(updateCourse);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCourseVM updateCourseVM)
        {
            ViewBag.categoryList = new SelectList(await _catalogService.GetAllCategoriesAsync(), "Id", "Name", updateCourseVM.Id);
            if (!ModelState.IsValid)
                return View();

            await _catalogService.UpdatdeCourseAsync(updateCourseVM);
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
