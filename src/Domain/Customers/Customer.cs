namespace Domain.Customers
{
    public class Customer
    {
        public Customer(Guid id, string name, string address, string email)
        {
            Id = id;
            Name = name;
            Address = address;
            Email = email;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }

        public void Update(string name, string address, string email)
        {
            Name = name;
            Address = address;
            Email = email;
        }
    }
}
