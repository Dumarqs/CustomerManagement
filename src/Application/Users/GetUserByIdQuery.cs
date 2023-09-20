using MediatR;

namespace Application.Users
{
    public record GetUserByIdQuery(Guid id) : IRequest<UserResponse>;

}
