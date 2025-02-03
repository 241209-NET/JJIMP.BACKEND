using JJIMP.API.Data;
using JJIMP.API.Model;
using Microsoft.EntityFrameworkCore;

namespace JJIMP.API.Repository;

public class UserRepository : IUserRepository
{
    private readonly JjimpContext _dbContext;

    public UserRepository(JjimpContext dbContext) => _dbContext = dbContext;

    public async Task<User?> GetUserById(int userId)
    {
        return await _dbContext
            .Users.Include(u => u.CreatedIssues)
            .Include(u => u.AssignedIssues)
            .Include(u => u.Comments)
            .Include(u => u.Projects)
            .Include(u => u.ManagedProjects)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbContext
            .Users.Include(u => u.CreatedIssues)
            .Include(u => u.AssignedIssues)
            .Include(u => u.Comments)
            .Include(u => u.Projects)
            .Include(u => u.ManagedProjects)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _dbContext
            .Users.Include(u => u.CreatedIssues)
            .Include(u => u.AssignedIssues)
            .Include(u => u.Comments)
            .Include(u => u.Projects)
            .Include(u => u.ManagedProjects)
            .Select(u => new User
            {
                Id = u.Id,
                Name = u.Name,
                Password = null!,
                Email = u.Email,
                Projects = u
                    .Projects.Select(p => new Project
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                    })
                    .ToList(),
                AssignedIssues = u
                    .AssignedIssues.Select(i => new Issue
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Description = i.Description,
                    })
                    .ToList(),
                CreatedIssues = u
                    .CreatedIssues.Select(i => new Issue
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Description = i.Description,
                    })
                    .ToList(),
                Comments = u
                    .Comments.Select(c => new Comment
                    {
                        Id = c.Id,
                        Content = c.Content,
                    })
                    .ToList(),
                ManagedProjects = u
                    .ManagedProjects.Select(p => new Project
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                    })
                    .ToList(),
            })
            .ToListAsync();
    }

    public async Task<User> CreateUser(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateUser(User user)
    {
        var userToUpdate = await _dbContext.Users.FindAsync(user.Id);
        if (userToUpdate == null)
        {
            return null;
        }
        if (user.Name != null)
        {
            user.Name = user.Name;
        }
        if (user.Email != null)
        {
            user.Email = user.Email;
        }
        if (user.Password != null)
        {
            user.Password = user.Password;
        }
        var updatedUser = _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
        return updatedUser.Entity;
    }

    public async Task<User?> DeleteUserById(int userId)
    {
        var user = await GetUserById(userId);
        if (user == null)
        {
            return null;
        }
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }
}
