using Course.Services.Catalog.DTOs.CategoryDTOs;
using Course.Services.Catalog.DTOs.FeatureDTOs;

namespace Course.Services.Catalog.DTOs.CourseDTOs
{
    public class CreateCourseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public FeatureDto Feature { get; set; }
        public string CategoryId { get; set; }
        public string UserId { get; set; }
    }
}
