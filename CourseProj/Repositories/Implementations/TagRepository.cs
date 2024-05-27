using CourseProj.Data;
using CourseProj.Models;
using CourseProj.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProj.Repositories.Implementations;

public class TagRepository(AppDbContext appDbContext) : ITagRepository
{
    public async Task<Tag> CreateTag(string value)
    {
        var existingTag = await appDbContext.Tags.FirstOrDefaultAsync(t => t.Name == value );
        if (existingTag == null)
        {
            var tag = new Tag{Name = value};
            appDbContext.Tags.Add(tag);
            await appDbContext.SaveChangesAsync();
            return tag;
        }

        return existingTag;
    }

    public async Task<IQueryable<string>> GetTagsStartsWith(string query)
    {
        var tags = appDbContext.Tags
            .Where(t => t.Name.StartsWith(query))
            .Select(t => t.Name);
        return tags;
    }

    public async Task<IQueryable<Tag>> GetTags()
    {
        var tags = appDbContext.Tags;

        return tags;
    }
}