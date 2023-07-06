namespace Course.Web.Models.FakePaymentModels
{
    public class PaymentInfoVM
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expration { get; set; }
        public string CVV { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
