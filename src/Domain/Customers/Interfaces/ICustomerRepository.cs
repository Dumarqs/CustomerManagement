namespace Domain.Customers.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
        Task DeleteAsync(Customer customer);
        Task<Customer?> GetByIdAsync(Guid id);
        Task<Customer> GetByEmailAsync(string email);
        Task UpdateAsync(Customer customer);

    }
}