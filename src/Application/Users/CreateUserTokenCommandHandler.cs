using Domain.Helpers;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Application.Users
{
    internal sealed class CreateUserTokenCommandHandler : IRequestHandler<CreateUserTokenCommand, TokenResponse>
    {
        private readonly JwtConfigurations _jwtConfigurations;

        public CreateUserTokenCommandHandler(JwtConfigurations jwtConfigurations)
        {
            _jwtConfigurations = jwtConfigurations;
        }

        public async Task<TokenResponse> Handle(CreateUserTokenCommand request, CancellationToken cancellationToken)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigurations.Secret));
            var securityToken = new JwtSecurityToken(
                issuer: _jwtConfigurations.Issuer,
                expires: DateTime.Now.AddHours(int.Parse(_jwtConfigurations.ExpiresHours)),
                claims: request.listAuthClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(securityToken);

            return new TokenResponse(token);
        }
    }
}
