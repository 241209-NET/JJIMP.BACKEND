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

    [Fact]
    public async Task GetProjectById_ShouldReturnProjectDTO()
    {
        // Arrange
        var project = _fixture.Create<Project>();
        var projectDTO = _fixture.Create<ProjectOutDTO>();
        var projectId = _fixture.Create<int>();

        _projectRepositoryMock.Setup(x => x.GetProjectById(It.IsAny<int>())).ReturnsAsync(project);
        _mapperMock.Setup(x => x.Map<ProjectOutDTO>(It.IsAny<Project>())).Returns(projectDTO);

        // Act
        var result = await _projectService.GetProjectById(projectId);

        // Assert
        Assert.Equal(projectDTO, result);
    }

    [Fact]
    public async Task GetProjectById_ShouldThrowIfProjectDoesNotExist()
    {
        // Arrange
        var projectId = _fixture.Create<int>();

        _projectRepositoryMock.Setup(x => x.GetProjectById(It.IsAny<int>())).ReturnsAsync(null as Project);

        // Act
        async Task act() => await _projectService.GetProjectById(projectId);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(act);
    }

    [Fact]
    public async Task CreateProject_ShouldReturnProjectDTO()
    {
        // Arrange
        var project = _fixture.Create<Project>();
        var projectDTO = _fixture.Create<ProjectOutDTO>();
        var createProjectDTO = _fixture.Create<CreateProjectDTO>();

        _projectRepositoryMock.Setup(x => x.CreateProject(It.IsAny<Project>())).ReturnsAsync(project);
        _mapperMock.Setup(x => x.Map<ProjectOutDTO>(It.IsAny<Project>())).Returns(projectDTO);

        // Act
        var result = await _projectService.CreateProject(createProjectDTO);

        // Assert
        Assert.Equal(projectDTO, result);
    }

    [Fact]
    public async Task UpdateProject_ShouldThrowIfProjectNotFound()
    {
        // Arrange
        var updateProjectDTO = _fixture.Create<UpdateProjectDTO>();

        _projectRepositoryMock.Setup(x => x.GetProjectById(It.IsAny<int>())).ReturnsAsync(null as Project);

        // Act
        async Task act() => await _projectService.UpdateProject(updateProjectDTO);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(act);
    }

    [Fact]
    public async Task UpdateProject_ShouldReturnProjectDTO()
    {
        // Arrange
        var project = _fixture.Create<Project>();
        var projectDTO = _fixture.Create<ProjectOutDTO>();
        var updateProjectDTO = _fixture.Create<UpdateProjectDTO>();

        _projectRepositoryMock.Setup(x => x.GetProjectById(It.IsAny<int>())).ReturnsAsync(project);
        _projectRepositoryMock.Setup(x => x.UpdateProject(It.IsAny<Project>(),It.IsAny<List<int>>())).ReturnsAsync(project);
        _mapperMock.Setup(x => x.Map<ProjectOutDTO>(It.IsAny<Project>())).Returns(projectDTO);

        // Act
        var result = await _projectService.UpdateProject(updateProjectDTO);

        // Assert
        Assert.Equal(projectDTO, result);
    }

    [Fact]
    public async Task DeleteProject_ShouldReturnProjectDTO()
    {
        // Arrange
        var project = _fixture.Create<Project>();
        var projectDTO = _fixture.Create<ProjectOutDTO>();
        var projectId = _fixture.Create<int>();

        _projectRepositoryMock.Setup(x => x.DeleteProject(It.IsAny<int>())).ReturnsAsync(project);
        _mapperMock.Setup(x => x.Map<ProjectOutDTO>(It.IsAny<Project>())).Returns(projectDTO);

        // Act
        var result = await _projectService.DeleteProject(projectId);

        // Assert
        Assert.Equal(projectDTO, result);
    }

    [Fact]
    public async Task DeleteProject_ShouldReturnNullIfProjectDoesNotExist()
    {
        // Arrange
        var projectId = _fixture.Create<int>();

        _projectRepositoryMock.Setup(x => x.DeleteProject(It.IsAny<int>())).ReturnsAsync(null as Project);

        // Act
        var result = await _projectService.DeleteProject(projectId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllProjects_ShouldReturnListOfProjectDTO()
    {
        // Arrange
        var projects = _fixture.CreateMany<Project>().ToList();
        var projectDTOs = _fixture.CreateMany<ProjectOutDTO>().ToList();

        _projectRepositoryMock.Setup(x => x.GetAllProjects()).ReturnsAsync(projects);
        _mapperMock.Setup(x => x.Map<IEnumerable<ProjectOutDTO>>(It.IsAny<IEnumerable<Project>>())).Returns(projectDTOs);

        // Act
        var result = await _projectService.GetAllProjects();

        // Assert
        Assert.Equal(projectDTOs, result);
    }



    [Fact]
    public async Task AddUserToProject_ShouldReturnProjectDTO()
    {
        // Arrange
        var project = _fixture.Create<Project>();
        var projectDTO = _fixture.Create<ProjectOutDTO>();
        var projectId = _fixture.Create<int>();
        var userId = _fixture.Create<int>();

        _projectRepositoryMock.Setup(x => x.AddUserToProject(It.IsAny<int>(),It.IsAny<int>())).ReturnsAsync(project);
        _mapperMock.Setup(x => x.Map<ProjectOutDTO>(It.IsAny<Project>())).Returns(projectDTO);

        // Act
        var result = await _projectService.AddUserToProject(projectId,userId);

        // Assert
        Assert.Equal(projectDTO, result);
    }
    
    [Fact]
    public async Task AddUserToProject_ShouldThrowIfProjectNotFound()
    {
        // Arrange
        var updateProjectDTO = _fixture.Create<UpdateProjectDTO>();
        var projectId = _fixture.Create<int>();
        var userId = _fixture.Create<int>();

        _projectRepositoryMock.Setup(x => x.GetProjectById(It.IsAny<int>())).ReturnsAsync(null as Project);

        // Act
        async Task act() => await _projectService.AddUserToProject(projectId,userId);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(act);
    }

}