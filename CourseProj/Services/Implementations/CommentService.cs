using CourseProj.Models;
using CourseProj.Repositories.Interfaces;
using CourseProj.Services.Interfaces;

namespace CourseProj.Services.Implementations;

public class CommentService(ICommentRepository commentRepository) : ICommentService
{
    public async Task<Comment> CreateComment(int itemId, string userId, string text)
    {
        var comment = new Comment { ItemId = itemId, AppUserId = userId, Text = text };

        comment = await commentRepository.CreateComment(comment);

        return comment;
    }

    public async Task DeleteComment(int itemId, string userId)
    {
        var comment = new Comment { ItemId = itemId, AppUserId = userId};

        await commentRepository.DeleteComment(comment);
    }
}