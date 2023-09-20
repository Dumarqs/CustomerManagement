using Dapper;
using Domain.Users;
using Domain.Users.Interfaces;
using Infra.Data.SqlServer.Helpers;

namespace Infra.Data.SqlServer.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly Session _session;
        public UserRepository(Session session)
        {
            _session = session;
        }

        public async Task AddAsync(User user)
        {
            await _session.Connection.ExecuteAsync(
                "INSERT INTO [User] (Id, Name, Login, Password, Role) VALUES(@Id, @Name, @Login, @Password, @Role)",
                param: new { @Id = user.Id, @Name = user.Name, @Login = user.Login, @Password = user.Password, @Role = user.Role },
                transaction: _session.Transaction);
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            return await _session.Connection.QueryFirstOrDefaultAsync<User>(
                "SELECT Id, Name, Login, Password, Role FROM [User] WHERE Login = @Login",
                param: new { @Login = login },
                transaction: _session.Transaction);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _session.Connection.QueryFirstOrDefaultAsync<User>(
                "SELECT Id, Name, Login, Password, Role FROM [User] WHERE Id = @Id",
                param: new { @Id = id },
                transaction: _session.Transaction);
        }

        public async Task<User> GetByNameAsync(string name)
        {
            return await _session.Connection.QueryFirstOrDefaultAsync<User>(
                "SELECT Id, Name, Login, Password, Role FROM [User] WHERE Name = @Name",
                param: new { @Name = name },
                transaction: _session.Transaction);
        }

        public async Task DeleteAsync(User user)
        {
            await _session.Connection.ExecuteAsync(
                "DELETE FROM [User] WHERE Id = @Id",
                param: new { @Id = user.Id },
                transaction: _session.Transaction);
        }
    }
}
