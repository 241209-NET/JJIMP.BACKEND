using JJIMP.API.DTO;

namespace JJIMP.API.Service;

public interface ICommentService
{
    Task<CommentOutDTO> GetCommentById(int id);
    Task<CommentOutDTO> CreateComment(CreateCommentDTO commentDTO);
    Task<CommentOutDTO> UpdateComment(UpdateCommentDTO commentDTO);
    Task<CommentOutDTO> DeleteComment(int id);
}