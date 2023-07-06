namespace Course.Web.Models.OrderModels
{
    public class OrderItemVM
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string PictureUrl { get; set; }
        public Decimal Price { get; set; }
    }
}
