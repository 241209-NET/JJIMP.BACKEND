using JJIMP.API.Model;
using JJIMP.API.DTO;
using JJIMP.API.Repository;
using JJIMP.API.Service;
using Moq;
using AutoMapper;
using AutoFixture;

namespace JJIMP.Test;

public class UserServiceTests
{
    private Mock<IUserRepository> _userRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private UserService _userService;
    private Fixture _fixture;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        _userService = new UserService(_userRepositoryMock.Object, _mapperMock.Object);

        _fixture = new Fixture();
        _fixture.Customize<DateOnly>(o => o.FromFactory((DateTime dt) => DateOnly.FromDateTime(dt)));
        _fixture.Customize<TimeOnly>(o => o.FromFactory((DateTime dt) => TimeOnly.FromDateTime(dt)));
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
    .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public async Task GetUserById_ShouldReturnUserDTO()
    {
        // Arrange
        var user = _fixture.Create<User>();
        var userDTO = _fixture.Create<UserOutDTO>();
        var userId = _fixture.Create<int>();

        _userRepositoryMock.Setup(x => x.GetUserById(It.IsAny<int>())).ReturnsAsync(user);
        _mapperMock.Setup(x => x.Map<UserOutDTO>(It.IsAny<User>())).Returns(userDTO);

        // Act
        var result = await _userService.GetUserById(userId);

        // Assert
        Assert.Equal(userDTO, result);
    }
    [Fact]
    public async Task GetUserById_ShouldThrowIfUserDoesNotExist()
    {
        // Arrange
        var userId = _fixture.Create<int>();

        _userRepositoryMock.Setup(x => x.GetUserById(It.IsAny<int>())).ReturnsAsync(null as User);

        // Act
        async Task act() => await _userService.GetUserById(userId);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(act);
    }

    [Fact]
    public async Task GetAllUsers_ShouldReturnUserDTOEnumerable()
    {
        // Arrange
        var users = _fixture.CreateMany<User>().ToList();
        var userDTOs = _fixture.CreateMany<UserOutDTO>().ToList();

        _userRepositoryMock.Setup(x => x.GetAllUsers()).ReturnsAsync(users);
        _mapperMock.Setup(x => x.Map<IEnumerable<UserOutDTO>>(It.IsAny<IEnumerable<User>>())).Returns(userDTOs);

        // Act
        var result = await _userService.GetAllUsers();

        // Assert
        Assert.Equal(userDTOs, result);
    }
    
    [Fact]
    public async Task CreateUser_ShouldReturnUserDTO()
    {
        // Arrange
        var user = _fixture.Create<User>();
        var userDTO = _fixture.Create<UserOutDTO>();
        var createUserDTO = _fixture.Create<CreateUserDTO>();

        _userRepositoryMock.Setup(x => x.CreateUser(It.IsAny<User>())).ReturnsAsync(user);
        _mapperMock.Setup(x => x.Map<User>(It.IsAny<CreateUserDTO>())).Returns(user);
        _mapperMock.Setup(x => x.Map<UserOutDTO>(It.IsAny<User>())).Returns(userDTO);

        // Act
        var result = await _userService.CreateUser(createUserDTO);

        // Assert
        Assert.Equal(userDTO, result);
    }

    [Fact]
    public async Task UpdateUser_ShouldReturnUserDTO()
    {
        // Arrange
        var user = _fixture.Create<User>();
        var userDTO = _fixture.Create<UserOutDTO>();
        var updateUserDTO = _fixture.Create<UpdateUserDTO>();

        _userRepositoryMock.Setup(x => x.GetUserById(It.IsAny<int>())).ReturnsAsync(user);
        _userRepositoryMock.Setup(x => x.UpdateUser(It.IsAny<User>())).ReturnsAsync(user);
        _mapperMock.Setup(x => x.Map<User>(It.IsAny<UpdateUserDTO>())).Returns(user);
        _mapperMock.Setup(x => x.Map<UserOutDTO>(It.IsAny<User>())).Returns(userDTO);

        // Act
        var result = await _userService.UpdateUser(updateUserDTO);

        // Assert
        Assert.Equal(userDTO, result);
    }


    [Fact]
    public async Task GetUserByName_ShouldReturnUserDTO()
    {
        // Arrange
        var user = _fixture.Create<User>();
        var userDTO = _fixture.Create<UserOutDTO>();
        var userName = _fixture.Create<string>();

        _userRepositoryMock.Setup(x => x.GetUserByName(It.IsAny<string>())).ReturnsAsync(user);
        _mapperMock.Setup(x => x.Map<UserOutDTO>(It.IsAny<User>())).Returns(userDTO);

        // Act
        var result = await _userService.GetUserByName(userName);

        // Assert
        Assert.Equal(userDTO, result);
    }
    [Fact]
    public async Task GetUserByName_ShouldThrowIfUserDoesNotExist()
    {
        // Arrange
        var userName = _fixture.Create<string>();

        _userRepositoryMock.Setup(x => x.GetUserByName(It.IsAny<string>())).ReturnsAsync(null as User);

        // Act
        async Task act() => await _userService.GetUserByName(userName);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(act);
    }


    [Fact]
    public async Task UpdateUser_ShouldThrowIfUserNotFound()
    {
        // Arrange
        var user = _fixture.Create<User>();
        var updateUserDTO = _fixture.Create<UpdateUserDTO>();

        _userRepositoryMock.Setup(x => x.GetUserById(It.IsAny<int>())).ReturnsAsync(null as User);
        _mapperMock.Setup(x => x.Map<User>(It.IsAny<UpdateUserDTO>())).Returns(user);

        // Act
        async Task act() => await _userService.UpdateUser(updateUserDTO);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(act);
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnUserDTO()
    {
        // Arrange
        var user = _fixture.Create<User>();
        var userDTO = _fixture.Create<UserOutDTO>();
        var userId = _fixture.Create<int>();

        _userRepositoryMock.Setup(x => x.DeleteUserById(It.IsAny<int>())).ReturnsAsync(user);
        _mapperMock.Setup(x => x.Map<UserOutDTO>(It.IsAny<User>())).Returns(userDTO);

        // Act
        var result = await _userService.DeleteUserById(userId);

        // Assert
        Assert.Equal(userDTO, result);
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnNullIfUserDoesNotExist()
    {
        // Arrange
        var userId = _fixture.Create<int>();

        _userRepositoryMock.Setup(x => x.DeleteUserById(It.IsAny<int>())).ReturnsAsync(null as User);

        // Act
        var result = await _userService.DeleteUserById(userId);

        // Assert
        Assert.Null(result);
    }

}