using Domain.Users.Interfaces;
using MediatR;

namespace Application.Users
{
    internal sealed class GetUserByNameQueryHandler : IRequestHandler<GetUserByNameQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByNameQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByNameAsync(request.Name);
            if (user is null)
            {
                return null;
            }
            return new UserResponse(user.Id, user.Name, user.Login, user.Password, user.Role);
        }
    }
}
