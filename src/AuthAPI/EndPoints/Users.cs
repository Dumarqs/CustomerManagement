using Application.Users;
using AuthAPI.ViewModels;
using AutoMapper;
using Carter;
using Domain.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AuthAPI.EndPoints
{
    public class Users : CarterModule
    {
        public Users() : base("/users") { }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/", async (CreateUserViewModel model, ISender sender, UserManager<ApplicationUser> userManager, ILogger<Users> logger) =>
            {
                var user = new ApplicationUser
                {
                    UserName = model.Name,
                    Email = model.Login,
                    PasswordHash = model.Password,
                    Role = "User"
                };

                var result = await userManager.CreateAsync(user);
                if(result.Succeeded)
                {
                    logger.LogInformation("User created a new account with password.");
                    return Results.Ok();
                }
                return Results.BadRequest(result.Errors);
            }).AllowAnonymous();

            app.MapDelete("/{login}", async (string login, ISender sender) =>
            {
                try
                {
                    await sender.Send(new DeleteUserCommand(login));

                    return Results.NoContent();
                }
                catch (UserNotFoundException ex)
                {
                    return Results.NotFound(ex.Message);
                }
            }).RequireAuthorization();

            app.MapPost("/Token", async (LoginViewModel model, ISender sender, 
                UserManager<ApplicationUser> userManager, ILogger<Users> logger,
                IMapper mapper) =>
            {
                var user = await userManager.FindByEmailAsync(model.Login);
                if (user is null)
                {
                    logger.LogInformation("User not found.");
                    return Results.BadRequest("User not found.");
                }

                var result = await userManager.CheckPasswordAsync(user, model.Password);
                if (result)
                {
                    var authClaims = new List<Claim>
                    { 
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                    var command = new CreateUserTokenCommand(authClaims);

                    logger.LogInformation("User logged in.");
                    return Results.Ok(sender.Send(command));
                }

                logger.LogInformation("User not found.");
                return Results.BadRequest("User not found.");
            }).AllowAnonymous();
        }
    }
}
