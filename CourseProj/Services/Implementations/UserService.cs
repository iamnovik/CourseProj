using CourseProj.Data;
using CourseProj.Models;
using CourseProj.Repositories.Interfaces;
using CourseProj.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProj.Services.Implementations;

public class  UserService(IUserRepository userRepository) : IUserService
{
    public async Task<List<AppUser>> GetUsers()
    {
        var users = await userRepository.GetUsers();
        return users;
    }

    public async Task<AppUser> GetUserById(string id)
    {
        var user = await userRepository.GetUserById(id);

        return user;
    }

    public async Task<AppUser> UpdateUserName(string value, string id)
    {
        var user = await userRepository.GetUserById(id);
        user.Name = value;
        await userRepository.Update(user);

        return user;
    }
}