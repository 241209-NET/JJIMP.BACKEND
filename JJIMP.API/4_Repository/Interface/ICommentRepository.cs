using JJIMP.API.Model;

namespace JJIMP.API.Repository;

public interface ICommentRepository
{
    Task<Comment?> GetCommentById(int commentId);
    Task<Comment> CreateComment(Comment comment);
    Task<Comment?> UpdateComment(Comment comment);
    Task<Comment?> DeleteComment(int commentId);
}