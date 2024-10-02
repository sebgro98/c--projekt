using AuthApi.Models;

namespace AuthApi.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> UpdateUserAsync(int id, User user);
        Task DeleteUserAsync(int id);
    }
}
