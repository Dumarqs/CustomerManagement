using MediatR;

namespace Application.Users
{
    public record class GetUserByNameQuery(string Name) : IRequest<UserResponse>;
}
