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
        return await _dbContext
            .Issues.Select(i => new Issue
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
            issue.CreatedAt = DateTime.Now;
            await _dbContext.Issues.AddAsync(issue);
            await _dbContext.SaveChangesAsync();

            //including asignee for frontend
            var createdIssue = await _dbContext
                .Issues.Include(i => i.Assignee)
                .FirstOrDefaultAsync(i => i.Id == issue.Id);

            return createdIssue;
        }
        catch (Exception)
        {
            return null!;
        }
    }

    public async Task<Issue?> UpdateIssue(Issue issueToUpdate)
    {
        var issue = await _dbContext.Issues.FindAsync(issueToUpdate.Id);
        if (issue == null)
        {
            return null!;
        }
        if (issueToUpdate.Title != null)
        {
            issue.Title = issueToUpdate.Title;
        }
        if (issueToUpdate.Description != null)
        {
            issue.Description = issueToUpdate.Description;
        }
        if (issueToUpdate.Deadline != null)
        {
            issue.Deadline = issueToUpdate.Deadline;
        }
        if (issueToUpdate.AssigneeId != null)
        {
            issue.AssigneeId = issueToUpdate.AssigneeId;
        }
        if (issueToUpdate.Status != null)
        {
            issue.Status = issueToUpdate.Status;
        }
        issue.UpdatedAt = DateTime.Now;
        var updatedIssue = _dbContext.Issues.Update(issue);
        await _dbContext.SaveChangesAsync();
        return updatedIssue.Entity;
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

    public async Task<IEnumerable<Issue>> GetAllIssues()
    {
        return await _dbContext
            .Issues.Include(i => i.Comments)
            .Include(i => i.Assignee)
            .Include(i => i.CreatedBy)
            .ToListAsync();
    }
}
