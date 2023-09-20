using Domain.Customers.Interfaces;
using Domain.Extensions;
using MediatR;

namespace Domain.Customers.Read
{
    internal sealed class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        
        public GetCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerResponse> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id);
            if(customer is null)
            {
                throw new CustomerNotFoundException(request.Id);
            }
            return new CustomerResponse(customer.Id, customer.Name, customer.Address, customer.Email);
        }
    }
}
