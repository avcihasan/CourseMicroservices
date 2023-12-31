﻿namespace Course.Web.Models.CatalogModels
{
    public class CourseVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription => Description.Length > 100 ? Description.Substring(0, 100) + "..." : Description;
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public string StockPictureUrl { get; set; }
        public DateTime CreatedTime { get; set; }

        public FeatureVM Feature { get; set; }
        public string CategoryId { get; set; }
        public string UserId { get; set; }
        public CategoryVM Category { get; set; }

    }
}
