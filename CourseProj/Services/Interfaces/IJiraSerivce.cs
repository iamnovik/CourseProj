using CourseProj.Models;

namespace CourseProj.Services.Interfaces;

public interface IJiraSerivce
{
    Task<string> CreateIssueAsync(string description, string priority, string collection, string link);
    Task<string> CreateUserAsync(string email, string displayName);

    Task<List<JiraIssue>> GetIssuesAsync();

}