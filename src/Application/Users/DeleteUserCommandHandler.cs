using Domain.Extensions;
using Domain.Helpers.Interfaces;
using Domain.Users.Interfaces;
using MediatR;

namespace Application.Users
{
    internal sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByLoginAsync(request.login);
            if (user is null)
            {
                throw new UserNotFoundException(request.login);
            }

            await _userRepository.DeleteAsync(user);
            _unitOfWork.Commit();
        }
    }
}
