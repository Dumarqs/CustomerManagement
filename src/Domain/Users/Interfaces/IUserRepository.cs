namespace Domain.Users.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User customer);
        Task DeleteAsync(User customer);
        Task<User?> GetByIdAsync(Guid id);
        Task<User> GetByLoginAsync(string login);
        Task<User> GetByNameAsync(string name);
    }
}
