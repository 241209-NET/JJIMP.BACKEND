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

    public async Task<IEnumerable<UserInfoOutDTO>> GetUsersByProjectId(int projectId)
    {
        var users = await _userRepository.GetUsersByProjectId(projectId);
        return _mapper.Map<IEnumerable<UserInfoOutDTO>>(users);
    }

    public async Task<UserOutDTO?> GetUserById(int userId)
    {
        var user = await _userRepository.GetUserById(userId) ?? throw new ArgumentException("User not found");
        return _mapper.Map<UserOutDTO?>(user);
    }

    public async Task<IEnumerable<UserInfoOutDTO>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();
        return _mapper.Map<IEnumerable<UserInfoOutDTO>>(users);
    }

    public async Task<UserInfoOutDTO?> GetUserInfoById(int userId)
    {
        var user = await _userRepository.GetUserInfoById(userId) ?? throw new ArgumentException("User not found");
        return _mapper.Map<UserInfoOutDTO?>(user);
    }

    public async Task<UserOutDTO> CreateUser(CreateUserDTO userDTO)
    {
        var user = _mapper.Map<User>(userDTO);
        var createdUser = await _userRepository.CreateUser(user);
        return _mapper.Map<UserOutDTO>(createdUser);
    }
    public async Task<UserOutDTO?> UpdateUser(UpdateUserDTO userDTO)
    {
        var userToUpdate = await _userRepository.GetUserById(userDTO.Id) ?? throw new ArgumentException("User not found");
        userToUpdate.Name = userDTO.Name;
        userToUpdate.Email = userDTO.Email;
        userToUpdate.Password = userDTO.Password;
        userToUpdate.Projects = userDTO.Projects;
        userToUpdate.CreatedIssues = userDTO.CreatedIssues;
        userToUpdate.AssignedIssues = userDTO.AssignedIssues;
        userToUpdate.Comments = userDTO.Comments;
        userToUpdate.ManagedProjects = userDTO.ManagedProjects;
        var updatedUser = await _userRepository.UpdateUser(userToUpdate);
        return _mapper.Map<UserOutDTO?>(updatedUser);

    }
    public async Task<UserOutDTO?> DeleteUserById(int userId)
    {
        var deletedUser = await _userRepository.DeleteUserById(userId);
        return _mapper.Map<UserOutDTO?>(deletedUser);
    }
}