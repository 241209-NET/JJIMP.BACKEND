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
        return await _dbContext.Users.FindAsync(userId);
    }
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }
    public async Task<User> CreateUser(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateUser(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }
    
    public async Task<User?> DeleteUserById(int userId)
    {
        var user = await GetUserById(userId);
        if(user == null)
        {
            return null;
        }
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }


}
