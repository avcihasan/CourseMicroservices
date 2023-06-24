namespace Course.Services.Discount.DTOs
{
    public class CreateDiscountDto
    {
        public string UserId { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
    }
}
