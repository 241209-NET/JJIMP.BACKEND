using JJIMP.API.Model;
using JJIMP.API.DTO;
using JJIMP.API.Repository;
using JJIMP.API.Service;
using Moq;
using AutoMapper;

namespace JJIMP.Test;

public class IssueServiceTests
{
    private Mock<IIssueRepository> _issueRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private IssueService _issueService;

    public IssueServiceTests()
    {
        _issueRepositoryMock = new Mock<IIssueRepository>();
        _mapperMock = new Mock<IMapper>();
        _issueService = new IssueService(_issueRepositoryMock.Object, _mapperMock.Object);
    }


}