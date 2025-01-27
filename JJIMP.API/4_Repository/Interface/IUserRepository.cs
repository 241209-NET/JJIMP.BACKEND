using JJIMP.API.Model;

namespace JJIMP.API.Repository;

public interface IUserRepository
{
    Task<User?> GetUserById(int userId);
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> CreateUser(User user);
    Task<User?> UpdateUser(User user);
    Task<User?> DeleteUserById(int userId);
}