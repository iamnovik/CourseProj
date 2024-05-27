using CourseProj.Models;

namespace CourseProj.Services.Interfaces;

public interface IUserService
{
    Task<List<AppUser>> GetUsers();

    Task<AppUser> GetUserById(string id);
}