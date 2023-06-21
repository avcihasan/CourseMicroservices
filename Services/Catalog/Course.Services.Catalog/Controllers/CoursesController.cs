using Course.Services.Catalog.DTOs.CourseDTOs;
using Course.Services.Catalog.Services.Abstractions;
using Course.Shared.CustomBaseController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : CustomBaseController
    {
        readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
            => CreateActionResult(await _courseService.GetAllCoursesAsync());
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById([FromRoute]string id)
            => CreateActionResult(await _courseService.GetCourseByIdAsync(id));
        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetAllCourseByUserId([FromRoute] string userId)
            => CreateActionResult(await _courseService.GetAllCoursesByUserIdAsync(userId));
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto course)
          => CreateActionResult(await _courseService.CreateCourseAsync(course));
        [HttpPut]
        public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseDto course)
          => CreateActionResult(await _courseService.UpdateCourseAsync(course));
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCourse([FromRoute] string id)
         => CreateActionResult(await _courseService.RemoveCourseAsync(id));
    }
}
