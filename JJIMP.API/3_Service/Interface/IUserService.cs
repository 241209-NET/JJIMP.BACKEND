using JJIMP.API.DTO;
using JJIMP.API.Model;

namespace JJIMP.API.Service;

public interface IUserService
{
    Task<UserOutDTO?> GetUserById(int userId);
    Task<IEnumerable<UserOutDTO>> GetAllUsers();
    Task<UserOutDTO> CreateUser(UserInDTO user);
    Task<UserOutDTO?> UpdateUser(UserInDTO user);
    Task<UserOutDTO?> DeleteUserById(int userId);
}