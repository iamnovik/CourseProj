using CourseProj.Models;

namespace CourseProj.Repositories.Interfaces;

public interface ILikeRepository
{
    Task<Like> CreateLike(Like like);
    Task DeleteLike(Like like);
}