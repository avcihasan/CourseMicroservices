using Course.Services.Catalog.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Course.Services.Catalog.DTOs.FeatureDTOs;
using Course.Services.Catalog.DTOs.CategoryDTOs;

namespace Course.Services.Catalog.DTOs.CourseDTOs
{
    public class CourseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedTime { get; set; }

        public FeatureDto Feature { get; set; }
        public string CategoryId { get; set; }
        public string UserId { get; set; }
        public CategoryDto Category { get; set; }
    }
}
