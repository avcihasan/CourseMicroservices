using Course.Services.Catalog.DTOs.CourseDTOs;
using Course.Shared.DTOs;

namespace Course.Services.Catalog.Services.Abstractions
{
    public interface ICourseService
    {
        Task<ResponseDto<List<CourseDto>>> GetAllCoursesAsync();
        Task<ResponseDto<CourseDto>> GetCourseByIdAsync(string id);
        Task<ResponseDto<List<CourseDto>>> GetAllCoursesByUserIdAsync(string userId);
        Task<ResponseDto<CourseDto>> CreateCourseAsync(CreateCourseDto course);
        Task<ResponseDto<NoContentDto>> UpdateCourseAsync(UpdateCourseDto course);
        Task<ResponseDto<NoContentDto>> RemoveCourseAsync(string id);
    }
}
