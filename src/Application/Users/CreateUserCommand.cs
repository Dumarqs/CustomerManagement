using MediatR;

namespace Application.Users
{
    public record CreateUserCommand(string Name, string Login, string Password, string Role) : IRequest<UserResponseId>;
}
