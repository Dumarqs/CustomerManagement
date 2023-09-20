using Domain.Users.Interfaces;
using MediatR;

namespace Application.Users
{
    internal sealed class GetUserByLoginQueryHandler : IRequestHandler<GetUserByLoginQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByLoginQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse?> Handle(GetUserByLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByLoginAsync(request.Login);
            if (user is null)
            {
                return null;                
            }
            return new UserResponse(user.Id, user.Name, user.Login, user.Password, user.Role);
        }
    }
}
