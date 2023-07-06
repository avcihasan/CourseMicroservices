namespace Course.Web.Models.OrderModels
{
    public class CreateOrderVM
    {
        public CreateOrderVM()
        {
            OrderItems = new List<OrderItemVM>();
        }
        public string BuyerId { get; set; }
        public List<OrderItemVM> OrderItems { get; set; }
        public AddressVM Address { get; set; }
    }
}
