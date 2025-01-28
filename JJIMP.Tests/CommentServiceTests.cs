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
    private Fixture _fixture;

    public CommentServiceTests()
    {
        _commentRepositoryMock = new Mock<ICommentRepository>();
        _mapperMock = new Mock<IMapper>();
        _commentService = new CommentService(_commentRepositoryMock.Object, _mapperMock.Object);

        _fixture = new Fixture();
        _fixture.Customize<DateOnly>(o => o.FromFactory((DateTime dt) => DateOnly.FromDateTime(dt)));
        _fixture.Customize<TimeOnly>(o => o.FromFactory((DateTime dt) => TimeOnly.FromDateTime(dt)));
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
    .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }


    [Fact]
    public async Task GetCommentById_ShouldReturnCommentDTO()
    {
        // Arrange
        var comment = _fixture.Create<Comment>();
        var commentDTO = _fixture.Create<CommentOutDTO>();
        var commentId = _fixture.Create<int>();

        _commentRepositoryMock.Setup(x => x.GetCommentById(It.IsAny<int>())).ReturnsAsync(comment);
        _mapperMock.Setup(x => x.Map<CommentOutDTO>(It.IsAny<Comment>())).Returns(commentDTO);

        // Act
        var result = await _commentService.GetCommentById(commentId);

        // Assert
        Assert.Equal(commentDTO, result);
    }

    [Fact]
    public async Task GetCommentById_ShouldThrowIfCommentDoesNotExist()
    {
        // Arrange
        var commentId = _fixture.Create<int>();

        _commentRepositoryMock.Setup(x => x.GetCommentById(It.IsAny<int>())).ReturnsAsync(null as Comment);

        // Act
        async Task act() => await _commentService.GetCommentById(commentId);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(act);
    }

    [Fact]
    public async Task CreateComment_ShouldReturnCommentDTO()
    {
        // Arrange
        var comment = _fixture.Create<Comment>();
        var commentDTO = _fixture.Create<CommentOutDTO>();
        var createCommentDTO = _fixture.Create<CreateCommentDTO>();

        _commentRepositoryMock.Setup(x => x.CreateComment(It.IsAny<Comment>())).ReturnsAsync(comment);
        _mapperMock.Setup(x => x.Map<CommentOutDTO>(It.IsAny<Comment>())).Returns(commentDTO);

        // Act
        var result = await _commentService.CreateComment(createCommentDTO);

        // Assert
        Assert.Equal(commentDTO, result);
    }

    [Fact]
    public async Task UpdateComment_ShouldThrowIfCommentNotFound()
    {
        // Arrange
        var updateCommentDTO = _fixture.Create<UpdateCommentDTO>();

        _commentRepositoryMock.Setup(x => x.GetCommentById(It.IsAny<int>())).ReturnsAsync(null as Comment);

        // Act
        async Task act() => await _commentService.UpdateComment(updateCommentDTO);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(act);
    }

    [Fact]
    public async Task UpdateComment_ShouldReturnCommentDTO()
    {
        // Arrange
        var comment = _fixture.Create<Comment>();
        var commentDTO = _fixture.Create<CommentOutDTO>();
        var updateCommentDTO = _fixture.Create<UpdateCommentDTO>();

        _commentRepositoryMock.Setup(x => x.GetCommentById(It.IsAny<int>())).ReturnsAsync(comment);
        _commentRepositoryMock.Setup(x => x.UpdateComment(It.IsAny<Comment>())).ReturnsAsync(comment);
        _mapperMock.Setup(x => x.Map<CommentOutDTO>(It.IsAny<Comment>())).Returns(commentDTO);

        // Act
        var result = await _commentService.UpdateComment(updateCommentDTO);

        // Assert
        Assert.Equal(commentDTO, result);
    }

    [Fact]
    public async Task DeleteComment_ShouldReturnCommentDTO()
    {
        // Arrange
        var comment = _fixture.Create<Comment>();
        var commentDTO = _fixture.Create<CommentOutDTO>();
        var commentId = _fixture.Create<int>();

        _commentRepositoryMock.Setup(x => x.DeleteComment(It.IsAny<int>())).ReturnsAsync(comment);
        _mapperMock.Setup(x => x.Map<CommentOutDTO>(It.IsAny<Comment>())).Returns(commentDTO);

        // Act
        var result = await _commentService.DeleteComment(commentId);

        // Assert
        Assert.Equal(commentDTO, result);
    }

    [Fact]
    public async Task DeleteComment_ShouldReturnNullIfCommentDoesNotExist()
    {
        // Arrange
        var commentId = _fixture.Create<int>();

        _commentRepositoryMock.Setup(x => x.DeleteComment(It.IsAny<int>())).ReturnsAsync(null as Comment);

        // Act
        var result = await _commentService.DeleteComment(commentId);

        // Assert
        Assert.Null(result);
    }
}