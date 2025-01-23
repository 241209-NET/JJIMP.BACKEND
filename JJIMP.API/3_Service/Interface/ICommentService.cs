using JJIMP.API.DTO;
using JJIMP.API.Model;

namespace JJIMP.API.Service;

public interface ICommentService
{

    Task<IEnumerable<CommentOutDTO>> GetCommentsByIssueId(int issueId);
    Task<CommentOutDTO> GetCommentById(int id);
    Task<CommentOutDTO> CreateComment(CreateCommentDTO commentDTO);
    Task<CommentOutDTO> UpdateComment(UpdateCommentDTO commentDTO);
    Task<CommentOutDTO> DeleteComment(int id);
}