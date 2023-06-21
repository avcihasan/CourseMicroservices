using Course.Services.Catalog.DTOs.CategoryDTOs;
using Course.Shared.DTOs;

namespace Course.Services.Catalog.Services.Abstractions
{
    public interface ICategoryService
    {
        Task<ResponseDto<List<CategoryDto>>> GetAllCategoriesAsync();
        Task<ResponseDto<CategoryDto>> GetCategorieByIdAsync(string id);
        Task<ResponseDto<CategoryDto>> CreateCategorieAsync(CreateCategoryDto category);

    }
}
