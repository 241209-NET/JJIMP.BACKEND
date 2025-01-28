using JJIMP.API.Model;
using JJIMP.API.DTO;
using JJIMP.API.Repository;
using JJIMP.API.Service;
using Moq;
using AutoMapper;
using AutoFixture;

namespace JJIMP.Test;

public class IssueServiceTests
{
    private Mock<IIssueRepository> _issueRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private IssueService _issueService;
    private Fixture _fixture;

    public IssueServiceTests()
    {
        _issueRepositoryMock = new Mock<IIssueRepository>();
        _mapperMock = new Mock<IMapper>();
        _issueService = new IssueService(_issueRepositoryMock.Object, _mapperMock.Object);

        _fixture = new Fixture();
        _fixture.Customize<DateOnly>(o => o.FromFactory((DateTime dt) => DateOnly.FromDateTime(dt)));
        _fixture.Customize<TimeOnly>(o => o.FromFactory((DateTime dt) => TimeOnly.FromDateTime(dt)));
    _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
        .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public async Task GetIssueById_ShouldReturnIssueDTO()
    {
        // Arrange
        var issue = _fixture.Create<Issue>();
        var issueDTO = _fixture.Create<IssueOutDTO>();
        var issueId = _fixture.Create<int>();

        _issueRepositoryMock.Setup(x => x.GetIssueById(It.IsAny<int>())).ReturnsAsync(issue);
        _mapperMock.Setup(x => x.Map<IssueOutDTO>(It.IsAny<Issue>())).Returns(issueDTO);

        // Act
        var result = await _issueService.GetIssueById(issueId);

        // Assert
        Assert.Equal(issueDTO, result);
    }

    [Fact]
    public async Task GetIssueById_ShouldThrowIfIssueDoesNotExist()
    {
        // Arrange
        var issueId = _fixture.Create<int>();

        _issueRepositoryMock.Setup(x => x.GetIssueById(It.IsAny<int>())).ReturnsAsync(null as Issue);

        // Act
        async Task act() => await _issueService.GetIssueById(issueId);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(act);
    }

    [Fact]
    public async Task CreateIssue_ShouldReturnIssueDTO()
    {
        // Arrange
        var issue = _fixture.Create<Issue>();
        var issueDTO = _fixture.Create<IssueOutDTO>();
        var createIssueDTO = _fixture.Create<CreateIssueDTO>();

        _issueRepositoryMock.Setup(x => x.CreateIssue(It.IsAny<Issue>())).ReturnsAsync(issue);
        _mapperMock.Setup(x => x.Map<IssueOutDTO>(It.IsAny<Issue>())).Returns(issueDTO);

        // Act
        var result = await _issueService.CreateIssue(createIssueDTO);

        // Assert
        Assert.Equal(issueDTO, result);
    }

    [Fact]
    public async Task UpdateIssue_ShouldThrowIfIssueNotFound()
    {
        // Arrange
        var updateIssueDTO = _fixture.Create<UpdateIssueDTO>();

        _issueRepositoryMock.Setup(x => x.GetIssueById(It.IsAny<int>())).ReturnsAsync(null as Issue);

        // Act
        async Task act() => await _issueService.UpdateIssue(updateIssueDTO);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(act);
    }

    [Fact]
    public async Task UpdateIssue_ShouldReturnIssueDTO()
    {
        // Arrange
        var issue = _fixture.Create<Issue>();
        var issueDTO = _fixture.Create<IssueOutDTO>();
        var updateIssueDTO = _fixture.Create<UpdateIssueDTO>();

        _issueRepositoryMock.Setup(x => x.GetIssueById(It.IsAny<int>())).ReturnsAsync(issue);
        _issueRepositoryMock.Setup(x => x.UpdateIssue(It.IsAny<Issue>())).ReturnsAsync(issue);
        _mapperMock.Setup(x => x.Map<IssueOutDTO>(It.IsAny<Issue>())).Returns(issueDTO);

        // Act
        var result = await _issueService.UpdateIssue(updateIssueDTO);

        // Assert
        Assert.Equal(issueDTO, result);
    }

    [Fact]
    public async Task DeleteIssue_ShouldReturnIssueDTO()
    {
        // Arrange
        var issue = _fixture.Create<Issue>();
        var issueDTO = _fixture.Create<IssueOutDTO>();
        var issueId = _fixture.Create<int>();

        _issueRepositoryMock.Setup(x => x.DeleteIssue(It.IsAny<int>())).ReturnsAsync(issue);
        _mapperMock.Setup(x => x.Map<IssueOutDTO>(It.IsAny<Issue>())).Returns(issueDTO);

        // Act
        var result = await _issueService.DeleteIssue(issueId);

        // Assert
        Assert.Equal(issueDTO, result);
    }

    [Fact]
    public async Task DeleteIssue_ShouldReturnNullIfIssueDoesNotExist()
    {
        // Arrange
        var issueId = _fixture.Create<int>();

        _issueRepositoryMock.Setup(x => x.DeleteIssue(It.IsAny<int>())).ReturnsAsync(null as Issue);

        // Act
        var result = await _issueService.DeleteIssue(issueId);

        // Assert
        Assert.Null(result);
    }
}
