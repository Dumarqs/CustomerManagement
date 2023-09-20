using Domain.Customers.Interfaces;
using Domain.Extensions;
using Domain.Helpers.Interfaces;
using MediatR;

namespace Domain.Customers.Update
{
    internal sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    { 
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id);
            if(customer is null)
            {
                throw new CustomerNotFoundException(request.Id);
            }
            customer.Update(request.Name, request.Address, request.Email);
            await _customerRepository.UpdateAsync(customer);

            _unitOfWork.Commit();
        }
    }
}
