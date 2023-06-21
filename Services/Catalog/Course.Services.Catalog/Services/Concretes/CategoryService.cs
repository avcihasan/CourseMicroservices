using AutoMapper;
using Course.Services.Catalog.DTOs.CategoryDTOs;
using Course.Services.Catalog.Entities;
using Course.Services.Catalog.Services.Abstractions;
using Course.Services.Catalog.Settings;
using Course.Shared.DTOs;
using MongoDB.Driver;
using System.Net;

namespace Course.Services.Catalog.Services.Concretes
{
    public class CategoryService : ICategoryService
    {
        readonly IMongoCollection<Category> _categoryCollection;
        readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            MongoClient client = new(databaseSettings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

            _mapper = mapper;
        }

        public async Task<ResponseDto<CategoryDto>> CreateCategorieAsync(CreateCategoryDto category)
        {
            await _categoryCollection.InsertOneAsync(_mapper.Map<Category>(category));
            return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), HttpStatusCode.OK);
        }

        public async Task<ResponseDto<List<CategoryDto>>> GetAllCategoriesAsync()
            => ResponseDto<List<CategoryDto>>.Success(
                _mapper.Map<List<CategoryDto>>(await _categoryCollection.Find(x => true).ToListAsync()), HttpStatusCode.OK);


        public async Task<ResponseDto<CategoryDto>> GetCategorieByIdAsync(string id)
        {
            Category category = await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (category is null)
                return ResponseDto<CategoryDto>.Fail("Category not found", HttpStatusCode.NotFound);
            return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), HttpStatusCode.OK);
        }

    }
}
