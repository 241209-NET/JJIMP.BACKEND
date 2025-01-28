using JJIMP.API.Model;

namespace JJIMP.API.Repository;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsersByProjectId(int projectId);
    Task<User?> GetUserById(int userId);
    Task<User?> GetUserByName(string userName);
    Task<IEnumerable<User>> GetAllUsers();
    Task<User?> GetUserInfoById(int userId);
    Task<User> CreateUser(User user);
    Task<User?> UpdateUser(User user);
    Task<User?> DeleteUserById(int userId);
}