using Domain.Customers.Interfaces;
using Domain.Helpers.Interfaces;
using MediatR;

namespace Domain.Customers.Create
{
    internal sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerResponseId> 
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, 
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerResponseId> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {           
            var customerId = new CustomerResponseId(Guid.NewGuid());
            var customer = new Customer(
                                customerId.Id,
                                request.Name,
                                request.Address,
                                request.Email);

            await _customerRepository.AddAsync(customer);
            _unitOfWork.Commit();

            return customerId;
        }
    }
}
