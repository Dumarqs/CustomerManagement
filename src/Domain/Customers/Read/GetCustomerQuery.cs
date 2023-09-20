using MediatR;

namespace Domain.Customers.Read
{
    public record GetCustomerQuery(Guid Id) : IRequest<CustomerResponse>;
    
    public record CustomerResponse(Guid Id, string Name, string Address, string Email);
}
