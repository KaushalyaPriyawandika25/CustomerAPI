namespace CustomerAPI.Models
{
    public class AddCustomerRequest
    {
        public string Name { get; set; }
        public long Phone { get; set; }
        public string Address { get; set; }
    }
}
