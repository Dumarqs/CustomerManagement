using MediatR;

namespace Domain.Customers.Delete
{
    public record DeleteCustomerCommand(Guid Id) : IRequest;
}
