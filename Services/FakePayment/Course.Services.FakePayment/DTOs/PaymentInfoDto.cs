namespace Course.Services.FakePayment.DTOs
{
    public class PaymentInfoDto
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expration { get; set; }
        public string CVV { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderDto Order { get; set; }
    }
}
