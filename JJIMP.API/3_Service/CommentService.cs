using AutoMapper;
using JJIMP.API.DTO;
using JJIMP.API.Model;
using JJIMP.API.Repository;

namespace JJIMP.API.Service;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CommentService(ICommentRepository commentRepository, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public async Task<CommentOutDTO> GetCommentById(int commentId)
    {
        var comment = await _commentRepository.GetCommentById(commentId) ?? throw new ArgumentException("Comment not found");
        return _mapper.Map<CommentOutDTO>(comment);
    }
    public async Task<CommentOutDTO> CreateComment(CreateCommentDTO commentDTO)
    {
        var comment = _mapper.Map<Comment>(commentDTO);
        var createdComment = await _commentRepository.CreateComment(comment);
        return _mapper.Map<CommentOutDTO>(createdComment);
    }

    public async Task<CommentOutDTO> UpdateComment(UpdateCommentDTO commentDTO)
    {
        var comment = _mapper.Map<Comment>(commentDTO);
        var commentToUpdate = await _commentRepository.GetCommentById(comment.Id) ?? throw new ArgumentException("Comment not found");
        commentToUpdate.Content = comment.Content;
        var updatedComment = await _commentRepository.UpdateComment(commentToUpdate);
        return _mapper.Map<CommentOutDTO>(updatedComment);
    }

    public async Task<CommentOutDTO> DeleteComment(int commentId)
    {
        var deletedComment = await _commentRepository.DeleteComment(commentId);
        return _mapper.Map<CommentOutDTO>(deletedComment);
    }
}