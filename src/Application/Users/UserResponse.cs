namespace Application.Users
{
    public record UserResponse(Guid Id, string Name, string Login, string Password, string Role);
}
