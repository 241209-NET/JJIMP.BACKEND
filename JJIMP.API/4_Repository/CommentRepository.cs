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
        return await _dbContext.Comments
            .Select(c => new Comment
            {
                Id = c.Id,
                Content = c.Content,
                PostedById = c.PostedById,
                PostedBy = new User
                {
                    Id = c.PostedBy.Id,
                    Name = c.PostedBy.Name,
                    Email = c.PostedBy.Email,
                    Password = null!,
                },
                IssueId = c.IssueId,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
            })
            .FirstOrDefaultAsync(c => c.Id == commentId);
    }

    public async Task<Comment> CreateComment(Comment comment)
    {
        try 
        {
            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
            var createdComment = await _dbContext.Comments.FindAsync(comment.Id);
            return createdComment!;
        }
        catch (Exception)
        {
            return null!;
        }
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
