using MediatR;
using System.Security.Claims;

namespace Application.Users
{
    public record CreateUserTokenCommand(List<Claim> listAuthClaims) : IRequest<TokenResponse>;
}
