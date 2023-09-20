using Domain.Customers.Interfaces;
using Domain.Helpers.Interfaces;
using MediatR;

namespace Domain.Customers.Create
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand> 
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, 
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(
                                Guid.NewGuid(),
                                request.Name,
                                request.Address,
                                request.Email);

            await _customerRepository.AddAsync(customer);
            _unitOfWork.Commit();
        }
    }
}
