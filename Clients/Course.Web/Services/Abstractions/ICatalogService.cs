using Course.Web.Models.CatalogModels;

namespace Course.Web.Services.Abstractions
{
    public interface ICatalogService
    {
        Task<List<CourseVM>> GetAllCoursesAsync();
        Task<List<CategoryVM>> GetAllCategoriesAsync();
        Task<List<CourseVM>> GetAllCoursesByUserIdAsync(string userId);
        Task<CourseVM> GetCourseByIdAsync(string courseId);
        Task<bool> CreateCourseAsync(CreateCourseVM createCourseVM);
        Task<bool> UpdatdeCourseAsync(UpdateCourseVM updateCourseVM);
        Task<bool> DeleteCourseAsync(string courseId);
    }
}
