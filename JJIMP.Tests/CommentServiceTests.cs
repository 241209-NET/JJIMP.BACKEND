using JJIMP.API.Model;
using JJIMP.API.DTO;
using JJIMP.API.Repository;
using JJIMP.API.Service;
using Moq;
using AutoMapper;
using AutoFixture;

namespace JJIMP.Test;

public class CommentServiceTests
{
    private Mock<ICommentRepository> _commentRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private CommentService _commentService;

    public CommentServiceTests()
    {
        _commentRepositoryMock = new Mock<ICommentRepository>();
        _mapperMock = new Mock<IMapper>();
        _commentService = new CommentService(_commentRepositoryMock.Object, _mapperMock.Object);
    }


}