using MediatR;

namespace Application.Users
{
    public record DeleteUserCommand(string login) : IRequest;
}
