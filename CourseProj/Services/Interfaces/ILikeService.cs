using CourseProj.Models;

namespace CourseProj.Services.Interfaces;

public interface ILikeService
{
    Task<Like> CreateLike(int itemId, string userId);
    Task DeleteLike(int itemId, string userId);
}