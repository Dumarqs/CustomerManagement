using MediatR;

namespace Domain.Customers.Create
{
    public record CreateCustomerCommand(string Name, string Address, string Email) : IRequest<CustomerResponseId>;

    public record CustomerResponseId(Guid Id);
}
