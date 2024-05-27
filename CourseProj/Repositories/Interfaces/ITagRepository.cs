using CourseProj.Models;

namespace CourseProj.Repositories.Interfaces;

public interface ITagRepository
{
    Task<Tag> CreateTag(string value);
    Task<IQueryable<string>> GetTagsStartsWith(string query);

    Task<IQueryable<Tag>> GetTags();
}