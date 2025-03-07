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
            .Issues.Include(i => i.Assignee)
            .Include(i => i.CreatedBy)
            .Include(i => i.Comments)
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

            return createdIssue!;
        }
        catch (Exception)
        {
            return null!;
        }
    }

    public async Task<Issue?> UpdateIssue(Issue issue)
    {
        var issueToUpdate = await _dbContext.Issues.FindAsync(issue.Id);
        if (issueToUpdate == null)
        {
            return null!;
        }
        if (issue.Title != null)
        {
            issueToUpdate.Title = issue.Title;
        }
        if (issue.Description != null)
        {
            issueToUpdate.Description = issue.Description;
        }
        if (issue.Deadline != null)
        {
            issueToUpdate.Deadline = issue.Deadline;
        }
        if (issue.AssigneeId != null)
        {
            issueToUpdate.AssigneeId = issue.AssigneeId;
        }
        issueToUpdate.Status = issue.Status;
        issueToUpdate.UpdatedAt = DateTime.Now;
        var updatedIssue = _dbContext.Issues.Update(issueToUpdate);
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
