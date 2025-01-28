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
        return await _dbContext.Issues.Include(i => i.Comments).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Issue> CreateIssue(Issue issue)
    {
        await _dbContext.Issues.AddAsync(issue);
        await _dbContext.SaveChangesAsync();
        return issue;
    }

    public async Task<Issue> UpdateIssue(Issue issue)
    {
        _dbContext.Issues.Update(issue);
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
