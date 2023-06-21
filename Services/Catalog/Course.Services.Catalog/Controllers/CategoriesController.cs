using Course.Services.Catalog.DTOs.CategoryDTOs;
using Course.Services.Catalog.Services.Abstractions;
using Course.Shared.CustomBaseController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomBaseController
    {
        readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
            => CreateActionResult(await _categoryService.GetAllCategoriesAsync());
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategorieById([FromRoute] string id)
           => CreateActionResult(await _categoryService.GetCategorieByIdAsync(id));
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto category)
           => CreateActionResult(await _categoryService.CreateCategorieAsync(category));
    }
}
