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


}