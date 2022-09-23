namespace CustomerAPI.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long Phone { get; set; }
        public  string Address { get; set; }

    }
}
