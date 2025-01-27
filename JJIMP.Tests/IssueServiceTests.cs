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


}