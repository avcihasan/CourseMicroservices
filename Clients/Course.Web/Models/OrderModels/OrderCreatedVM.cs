namespace Course.Web.Models.OrderModels
{
    public class OrderCreatedVM
    {
        public int Id { get; set; }
        public string Error { get; set; }
        public bool IsSuccessful { get; set; } = true;
    }
}
