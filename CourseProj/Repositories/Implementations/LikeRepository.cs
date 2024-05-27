using CourseProj.Data;
using CourseProj.Models;
using CourseProj.Repositories.Interfaces;

namespace CourseProj.Repositories.Implementations;

public class LikeRepository(AppDbContext appDbContext) : ILikeRepository
{
    public async Task<Like> CreateLike(Like like)
    {
        appDbContext.Likes.Add(like);

        await appDbContext.SaveChangesAsync();

        return like;
    }

    public async Task DeleteLike(Like like)
    {
        appDbContext.Likes.Remove(like);

        await appDbContext.SaveChangesAsync();
        
    }
}