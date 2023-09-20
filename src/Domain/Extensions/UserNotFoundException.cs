namespace Domain.Extensions
{
    public sealed class UserNotFoundException : Exception
    {
        public UserNotFoundException(string login) : base($"The user login {login} is not registered.")
        {
        }
    }
}
