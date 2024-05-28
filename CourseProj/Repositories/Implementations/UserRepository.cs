using CourseProj.Data;
using CourseProj.Models;
using CourseProj.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProj.Repositories.Implementations;

public class UserRepository(AppDbContext appDbContext) : IUserRepository
{
    public async Task<List<AppUser>> GetUsers()
    {
        var users = await appDbContext.Users.ToListAsync();
        return users;
    }

    public async Task<AppUser> GetUserById(string id)
    {
        var user = await appDbContext.Users.FindAsync(id);

        return user;
    }

    public async Task<AppUser> Update(AppUser user)
    {
        appDbContext.Users.Update(user);

        await appDbContext.SaveChangesAsync();

        return user;
    }
}