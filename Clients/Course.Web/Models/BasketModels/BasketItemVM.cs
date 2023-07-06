namespace Course.Web.Models.BasketModels
{
    public class BasketItemVM
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal CoursePrice { get; set; }
        private decimal? DiscountAppliedPrice { get; set; }
        public decimal CurrentPrice => DiscountAppliedPrice != null ? DiscountAppliedPrice.Value : CoursePrice;

        public void AppliedDiscount(decimal discountPrice) 
            => DiscountAppliedPrice = discountPrice;
    }
}
