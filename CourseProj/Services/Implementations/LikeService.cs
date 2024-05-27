using CourseProj.Models;
using CourseProj.Repositories.Interfaces;
using CourseProj.Services.Interfaces;

namespace CourseProj.Services.Implementations;

public class LikeService(ILikeRepository likeRepository) : ILikeService
{
    public async Task<Like> CreateLike(int itemId, string userId)
    {
        var like = new Like { AppUserId = userId, ItemId = itemId };
        like = await likeRepository.CreateLike(like);
        return like;
    }

    public async Task DeleteLike(int itemId, string userId)
    {
        var like = new Like { AppUserId = userId, ItemId = itemId };
        await likeRepository.DeleteLike(like);
    }
}