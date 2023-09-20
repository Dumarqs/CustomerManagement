namespace Domain.Users
{
    public class User
    {
        public User(Guid id, string name, string login, string password, string role)
        {
            Id = id;
            Name = name;
            Login = login;
            Password = password;
            Role = role;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; } 
        public string Role { get; private set; }
    }
}
