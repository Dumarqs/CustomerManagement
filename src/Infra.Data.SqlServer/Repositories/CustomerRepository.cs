using Dapper;
using Domain.Customers;
using Domain.Customers.Interfaces;
using Infra.Data.SqlServer.Helpers;
using static Dapper.SqlMapper;

namespace Infra.Data.SqlServer.Repositories
{
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly Session _session;
        public CustomerRepository(Session session)
        {
            _session = session;
        }

        public async Task AddAsync(Customer customer)
        {
            await _session.Connection.ExecuteAsync(
                    "INSERT INTO Customer(Id, Name, Address, Email) VALUES(@Id, @Name, @Address, @Email)",
                    param: new { @Id = customer.Id, @Name = customer.Name, @Address = customer.Address, @Email = customer.Email },
                    transaction: _session.Transaction
            );
        }

        public async Task DeleteAsync(Customer customer)
        {
            await _session.Connection.ExecuteAsync(
                    "DELETE FROM Customer WHERE Id = @Id",
                    param: new { @Id = customer.Id },
                    transaction: _session.Transaction);
        }

        public async Task<Customer> GetByEmailAsync(string email)
        {
            return await _session.Connection.QueryFirstOrDefaultAsync<Customer>(
                    "SELECT Id, Name, Address, Email FROM Customer WHERE Email = @Email",
                    param: new { @Email = email },
                    transaction: _session.Transaction);
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _session.Connection.QueryFirstOrDefaultAsync<Customer>(
                    "SELECT Id, Name, Address, Email FROM Customer WHERE Id = @Id",
                    param: new { @Id = id },
                    transaction: _session.Transaction);
        }

        public async Task UpdateAsync(Customer customer)
        {
            await _session.Connection.ExecuteAsync(
                    "UPDATE Customer SET Name = @Name, Address = @Address, Email = @Email WHERE Id = @Id",
                    param: new { @Id = customer.Id, @Name = customer.Name, @Address = customer.Address, @Email = customer.Email },
                    transaction: _session.Transaction);
        }
    }
}
