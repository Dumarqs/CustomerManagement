using Domain.Helpers.Interfaces;
using Domain.Users;
using Domain.Users.Interfaces;
using MediatR;

namespace Application.Users
{
    internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponseId>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUserRepository userRepository, 
                       IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserResponseId> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = new UserResponseId(Guid.NewGuid());
            var user = new User(
                            userId.id, 
                            request.Name,
                            request.Login, 
                            request.Password, 
                            request.Role);

            await _userRepository.AddAsync(user);
            _unitOfWork.Commit();

            return userId;
        }
    }
}
