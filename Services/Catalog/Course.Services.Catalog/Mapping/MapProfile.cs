using AutoMapper;
using Course.Services.Catalog.DTOs.CategoryDTOs;
using Course.Services.Catalog.DTOs.CourseDTOs;
using Course.Services.Catalog.DTOs.FeatureDTOs;
using Course.Services.Catalog.Entities;

namespace Course.Services.Catalog.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Entities.Course, CourseDto>().ReverseMap();
            CreateMap<Entities.Course, CreateCourseDto>().ReverseMap();
            CreateMap<Entities.Course, UpdateCourseDto>().ReverseMap();
            CreateMap<CourseDto, CreateCourseDto>().ReverseMap();

            CreateMap<Feature, FeatureDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<CategoryDto, CreateCategoryDto>().ReverseMap();
        }
    }
}
