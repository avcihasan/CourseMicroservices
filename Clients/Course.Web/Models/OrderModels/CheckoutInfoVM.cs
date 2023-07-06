namespace Course.Web.Models.OrderModels
{
    public class CheckoutInfoVM
    {
        public AddressVM Address { get; set; }

        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
    }
}
