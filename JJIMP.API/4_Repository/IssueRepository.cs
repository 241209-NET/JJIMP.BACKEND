using JJIMP.API.Data;
using JJIMP.API.Model;
using Microsoft.EntityFrameworkCore;

namespace JJIMP.API.Repository;

public class IssueRepository : IIssueRepository
{
    private readonly JjimpContext _dbContext;

    public IssueRepository(JjimpContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Issue?> GetIssueById(int id)
    {
        return await _dbContext.Issues
            .Select(i => new Issue
            {
                Id = i.Id,
                Title = i.Title,
                Description = i.Description,
                Deadline = i.Deadline,
                AssigneeId = i.AssigneeId,
                Status = i.Status,
                Assignee = new User
                {
                    Id = i.Assignee.Id,
                    Name = i.Assignee.Name,
                    Password = null!,
                    Email = i.Assignee.Email,
                },
                CreatedBy = new User
                {
                    Id = i.CreatedBy.Id,
                    Name = i.CreatedBy.Name,
                    Password = null!,
                    Email = i.CreatedBy.Email,
                },
                ProjectId = i.ProjectId,
                Comments = i.Comments,
                CreatedAt = i.CreatedAt,
                UpdatedAt = i.UpdatedAt,
            })
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Issue> CreateIssue(Issue issue)
    {
        try
        {
            await _dbContext.Issues.AddAsync(issue);
            await _dbContext.SaveChangesAsync();
            return issue;
        }
        catch (Exception)
        {
            return null!;
        }
    }

    public async Task<Issue> UpdateIssue(Issue issue)
    {
        await _dbContext.SaveChangesAsync();
        return issue;
    }

    public async Task<Issue?> DeleteIssue(int id)
    {
        var issue = await _dbContext.Issues.FindAsync(id);
        if (issue != null)
        {
            _dbContext.Issues.Remove(issue);
            await _dbContext.SaveChangesAsync();
        }
        return issue;
    }
}
