using CourseProj.Models;

namespace CourseProj.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<AppUser>> GetUsers();

    Task<AppUser> GetUserById(string id);
    
    Task<AppUser> Update(AppUser user);
}