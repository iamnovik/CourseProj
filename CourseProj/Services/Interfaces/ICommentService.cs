using CourseProj.Models;

namespace CourseProj.Services.Interfaces;

public interface ICommentService
{
    Task<Comment> CreateComment(int itemId, string userId, string text);
    Task DeleteComment(int itemId, string userId);
}