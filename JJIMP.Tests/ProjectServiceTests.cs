using JJIMP.API.Model;
using JJIMP.API.DTO;
using JJIMP.API.Repository;
using JJIMP.API.Service;
using Moq;
using AutoMapper;

namespace JJIMP.Test;

public class ProjectServiceTests
{
    private Mock<IProjectRepository> _projectRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private ProjectService _projectService;

    public ProjectServiceTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _mapperMock = new Mock<IMapper>();
        _projectService = new ProjectService(_projectRepositoryMock.Object, _mapperMock.Object);
    }


}