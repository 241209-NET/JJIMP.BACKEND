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

    public async Task<Comment?> GetCommentById(int commentId)
    {
        return await _dbContext
            .Comments.Include(c => c.PostedBy)
            .FirstOrDefaultAsync(c => c.Id == commentId);
    }

    public async Task<Comment> CreateComment(Comment comment)
    {
        try
        {
            comment.CreatedAt = DateTime.Now;
            comment.UpdatedAt = DateTime.Now;
            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();

            // Refetching the comment with PostedBy included so it doesn't break the front end
            var createdComment = await _dbContext
                .Comments.Include(c => c.PostedBy)
                .FirstOrDefaultAsync(c => c.Id == comment.Id);

            return createdComment!;
        }
        catch (Exception)
        {
            return null!;
        }
    }

    public async Task<Comment?> UpdateComment(Comment commentToUpdate)
    {
        var comment = await _dbContext.Comments.FindAsync(commentToUpdate.Id);
        if (comment == null)
        {
            return null;
        }

        if (commentToUpdate.Content != null)
        {
            comment.Content = commentToUpdate.Content;
        }
        comment.UpdatedAt = DateTime.Now;

        var updatedComment = _dbContext.Comments.Update(comment);
        await _dbContext.SaveChangesAsync();
        return updatedComment.Entity;
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
