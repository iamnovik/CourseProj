using CourseProj.Data;
using CourseProj.Models;
using CourseProj.Repositories.Interfaces;
using CourseProj.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProj.Services.Implementations;

public class TagService(ITagRepository tagRepository) : ITagService
{
    public async Task<Tag> CreateTag(string value)
    {
        var tag = await tagRepository.CreateTag(value);
        return tag;
    }

    public async Task<IQueryable<string>> GetTagsStartsWith(string query)
    {
        var tags = await tagRepository.GetTagsStartsWith(query);
        return tags;
    }

    public async Task<IQueryable<Tag>> GetTags()
    {
        var tags = await tagRepository.GetTags();
        return tags;
    }
}