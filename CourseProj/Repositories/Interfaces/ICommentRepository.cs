using CourseProj.Models;

namespace CourseProj.Repositories.Interfaces;

public interface ICommentRepository
{
    Task<Comment> CreateComment(Comment comment);
    Task DeleteComment(Comment comment);
}