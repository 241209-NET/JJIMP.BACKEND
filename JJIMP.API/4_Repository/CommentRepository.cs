using JJIMP.API.Data;
using JJIMP.API.Model;
using Microsoft.EntityFrameworkCore;

namespace JJIMP.API.Repository;

public class CommentRepository : ICommentRepository
{
    public readonly JjimpContext _dbContext;

    public CommentRepository(JjimpContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Comment>> GetCommentsByIssueId(int issueId)
    {
        return await _dbContext.Comments.Where(c => c.IssueId == issueId).ToListAsync();
    }

    public async Task<Comment?> GetCommentById(int commentId)
    {
        return await _dbContext.Comments.FindAsync(commentId);
    }

    public async Task<Comment> CreateComment(Comment comment)
    {
        await _dbContext.Comments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();
        var createdComment = await _dbContext.Comments.FindAsync(comment.Id);
        return createdComment!;
    }

    public async Task<Comment> UpdateComment(Comment comment)
    {
        _dbContext.Comments.Update(comment);
        await _dbContext.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> DeleteComment(int commentId)
    {
        var comment = await _dbContext.Comments.FindAsync(commentId);
        if (comment == null)
        {
            return null;
        }

        _dbContext.Comments.Remove(comment);
        await _dbContext.SaveChangesAsync();
        return comment;
    }
}
