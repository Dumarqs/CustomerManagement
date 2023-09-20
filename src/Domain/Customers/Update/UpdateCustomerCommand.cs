using MediatR;

namespace Domain.Customers.Update
{
    public record UpdateCustomerCommand(Guid Id, string Name, string Address, string Email) : IRequest;
}
