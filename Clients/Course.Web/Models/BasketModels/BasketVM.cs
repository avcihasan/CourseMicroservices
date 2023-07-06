namespace Course.Web.Models.BasketModels
{
    public class BasketVM
    {
        public BasketVM()
        {
            BasketItems = new List<BasketItemVM>();
        }
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public int? DiscountRate { get; set; }
        private List<BasketItemVM> _basketItems { get; set; }
        public List<BasketItemVM> BasketItems
        {
            get
            {
                if (HasDiscount)
                {
                    _basketItems.ForEach(x =>
                    {
                        var discountPrice = x.CoursePrice * ((decimal)DiscountRate.Value / 100);
                        x.AppliedDiscount(Math.Round(x.CoursePrice - discountPrice, 2));
                    });
                }
                return _basketItems;
            } set
            {
                _basketItems=value;
            }
        }
        public decimal TotalPrice => BasketItems.Sum(x => x.CurrentPrice);

        public bool HasDiscount => !string.IsNullOrEmpty(DiscountCode) && DiscountRate.HasValue;
    }
}
