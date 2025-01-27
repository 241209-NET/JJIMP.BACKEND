using JJIMP.API.DTO;
using JJIMP.API.Model;

namespace JJIMP.API.Service;

public interface IUserService
{
    Task<IEnumerable<UserInfoOutDTO>> GetUsersByProjectId(int projectId);
    Task<UserOutDTO?> GetUserById(int userId);
    Task<IEnumerable<UserInfoOutDTO>> GetAllUsers();
    Task<UserInfoOutDTO?> GetUserInfoById(int userId);
    Task<UserOutDTO> CreateUser(CreateUserDTO user);
    Task<UserOutDTO?> UpdateUser(UpdateUserDTO user);
    Task<UserOutDTO?> DeleteUserById(int userId);
}