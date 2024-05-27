using CourseProj.Data;
using CourseProj.Models;
using CourseProj.Repositories.Interfaces;

namespace CourseProj.Repositories.Implementations;

public class CommentRepository(AppDbContext appDbContext) : ICommentRepository
{
    public async Task<Comment> CreateComment(Comment comment)
    {
        appDbContext.Comments.Add(comment);

        await appDbContext.SaveChangesAsync();

        return comment;
    }

    public async Task DeleteComment(Comment comment)
    {
        appDbContext.Comments.Remove(comment);
        await appDbContext.SaveChangesAsync();
    }
}