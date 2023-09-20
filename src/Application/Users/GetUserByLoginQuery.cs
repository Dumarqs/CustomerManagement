using MediatR;

namespace Application.Users
{
    public record GetUserByLoginQuery(string Login) : IRequest<UserResponse>;
}
