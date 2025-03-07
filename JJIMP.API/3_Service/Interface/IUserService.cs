using JJIMP.API.DTO;
using JJIMP.API.Model;

namespace JJIMP.API.Service;

public interface IUserService
{
    Task<UserOutDTO?> GetUserById(int userId);
    Task<UserOutDTO?> AuthenticateUser(LoginUserDTO user);
    Task<IEnumerable<UserOutDTO>> GetAllUsers();
    Task<UserOutDTO> CreateUser(CreateUserDTO user);
    Task<UserOutDTO?> UpdateUser(UpdateUserDTO user);
    Task<UserOutDTO?> DeleteUserById(int userId);
}
