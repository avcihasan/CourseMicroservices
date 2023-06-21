using AutoMapper;
using Course.Services.Catalog.DTOs.CategoryDTOs;
using Course.Services.Catalog.DTOs.CourseDTOs;
using Course.Services.Catalog.Entities;
using Course.Services.Catalog.Services.Abstractions;
using Course.Services.Catalog.Settings;
using Course.Shared.DTOs;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Net;

namespace Course.Services.Catalog.Services.Concretes
{
    public class CourseService : ICourseService
    {
        readonly IMongoCollection<Entities.Course> _courseCollection;
        readonly IMapper _mapper;
        readonly ICategoryService _categoryService;
        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings, ICategoryService categoryService)
        {
            MongoClient client = new(databaseSettings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Entities.Course>(databaseSettings.CourseCollectionName);
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public async Task<ResponseDto<CourseDto>> CreateCourseAsync(CreateCourseDto course)
        {
            await _courseCollection.InsertOneAsync(_mapper.Map<Entities.Course>(course));
            return ResponseDto<CourseDto>.Success(_mapper.Map<CourseDto>(course), HttpStatusCode.OK);
        }

        public async Task<ResponseDto<List<CourseDto>>> GetAllCoursesAsync()
        {
            List<Entities.Course> courses = await _courseCollection.Find(x => true).ToListAsync();
            if (courses.Any())
                foreach (var course in courses)
                    course.Category = _mapper.Map<Category>((await _categoryService.GetCategorieByIdAsync(course.CategoryId)).Data);
            else
                courses = new();
            return ResponseDto<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), HttpStatusCode.OK);
        }

        public async Task<ResponseDto<List<CourseDto>>> GetAllCoursesByUserIdAsync(string userId)
        {
            List<Entities.Course> courses = await _courseCollection.Find(x => x.UserId == userId).ToListAsync();
            if (courses.Any())
                foreach (var course in courses)
                    course.Category = _mapper.Map<Category>((await _categoryService.GetCategorieByIdAsync(course.CategoryId)).Data);
            else
                courses = new();
            return ResponseDto<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), HttpStatusCode.OK);
        }

        public async Task<ResponseDto<CourseDto>> GetCourseByIdAsync(string id)
        {
            Entities.Course course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (course is null)
                return ResponseDto<CourseDto>.Fail("Course not found", HttpStatusCode.NotFound);
            CourseDto courseDto = _mapper.Map<CourseDto>(course);
            courseDto.Category = (await _categoryService.GetCategorieByIdAsync(courseDto.CategoryId)).Data;
            return ResponseDto<CourseDto>.Success(courseDto, HttpStatusCode.OK);
        }

        public async Task<ResponseDto<NoContentDto>> RemoveCourseAsync(string id)
        {
            DeleteResult result = await _courseCollection.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount <= 0)
                return ResponseDto<NoContentDto>.Fail("Course not found", HttpStatusCode.NotFound);
            return ResponseDto<NoContentDto>.Success(HttpStatusCode.NoContent);
        }

        public async Task<ResponseDto<NoContentDto>> UpdateCourseAsync(UpdateCourseDto course)
        {
           ReplaceOneResult result=await _courseCollection.ReplaceOneAsync(x => x.Id == course.Id, _mapper.Map<Entities.Course>(course));
            if (result.ModifiedCount <= 0)
                return ResponseDto<NoContentDto>.Fail("Course not found", HttpStatusCode.NotFound);
            return ResponseDto<NoContentDto>.Success(HttpStatusCode.NoContent);
        }
    }
}
