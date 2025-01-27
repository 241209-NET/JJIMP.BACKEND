using JJIMP.API.Model;
using JJIMP.API.DTO;
using JJIMP.API.Repository;
using JJIMP.API.Service;
using Moq;
using AutoMapper;
using AutoFixture;

namespace JJIMP.Test;

public class ProjectServiceTests
{
    private Mock<IProjectRepository> _projectRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private ProjectService _projectService;
    private Fixture _fixture;

    public ProjectServiceTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _mapperMock = new Mock<IMapper>();
        _projectService = new ProjectService(_projectRepositoryMock.Object, _mapperMock.Object);

        _fixture = new Fixture();
        _fixture.Customize<DateOnly>(o => o.FromFactory((DateTime dt) => DateOnly.FromDateTime(dt)));
        _fixture.Customize<TimeOnly>(o => o.FromFactory((DateTime dt) => TimeOnly.FromDateTime(dt)));
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
    .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }


}