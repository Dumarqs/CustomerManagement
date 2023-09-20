using Domain.Users.Interfaces;
using MediatR;

namespace Application.Users
{
    internal sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.id);
            if (user is null)
            {
                return null;                
            }
            return new UserResponse(user.Id, user.Name, user.Login, user.Password, user.Role);
        }
    }
}
