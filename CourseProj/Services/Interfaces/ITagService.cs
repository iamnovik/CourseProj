using CourseProj.Models;

namespace CourseProj.Services.Interfaces;

public interface ITagService
{
    Task<Tag> CreateTag(string value);
    Task<IQueryable<string>> GetTagsStartsWith(string query);

    Task<IQueryable<Tag>> GetTags();
}