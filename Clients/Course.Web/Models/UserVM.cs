namespace Course.Web.Models
{
    public class UserVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public IEnumerable<string> GetUserProperties()
        {
            yield return Id;
            yield return UserName;
            yield return Email;
            yield return PhoneNumber;
        }
    }
}
