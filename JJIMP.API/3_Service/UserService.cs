using AutoMapper;
using JJIMP.API.DTO;
using JJIMP.API.Model;
using JJIMP.API.Repository;

namespace JJIMP.API.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserOutDTO?> GetUserById(int userId)
    {
        var user = await _userRepository.GetUserById(userId) ?? throw new ArgumentException("User not found");
        return _mapper.Map<UserOutDTO?>(user);
    }
    public async Task<UserOutDTO?> GetUserByName(string userName)
    {
        var user = await _userRepository.GetUserByName(userName) ?? throw new ArgumentException("User not found");;
        return _mapper.Map<UserOutDTO?>(user);
    }

    public async Task<IEnumerable<UserOutDTO>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();
        return _mapper.Map<IEnumerable<UserOutDTO>>(users);
    }

    public async Task<UserOutDTO> CreateUser(CreateUserDTO userDTO)
    {
        var user = _mapper.Map<User>(userDTO);
        user.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
        var createdUser = await _userRepository.CreateUser(user);
        return _mapper.Map<UserOutDTO>(createdUser);
    }

    public async Task<UserOutDTO?> UpdateUser(UpdateUserDTO userDTO)
    {
        var userToUpdate = _mapper.Map<User>(userDTO);
        userToUpdate.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
        var updatedUser = await _userRepository.UpdateUser(userToUpdate) ?? throw new ArgumentException("User not found");
        return _mapper.Map<UserOutDTO>(updatedUser);
    }

    public async Task<UserOutDTO?> DeleteUserById(int userId)
    {
        var deletedUser = await _userRepository.DeleteUserById(userId);
        return _mapper.Map<UserOutDTO?>(deletedUser);
    }
}