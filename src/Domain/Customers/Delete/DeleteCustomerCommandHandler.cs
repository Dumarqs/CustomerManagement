using Domain.Customers.Interfaces;
using Domain.Extensions;
using Domain.Helpers.Interfaces;
using MediatR;

namespace Domain.Customers.Delete
{
    internal sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id);
            if (customer is null)
            {
                throw new CustomerNotFoundException(request.Id);
            }
            
            await _customerRepository.DeleteAsync(customer);
            _unitOfWork.Commit();
        }
    }
}
