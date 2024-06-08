using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using CourseProj.Models;
using CourseProj.Models.Enums;
using CourseProj.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace CourseProj.Services.Implementations;

public class JiraService : IJiraSerivce
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly string _jiraBaseUrl;
    private readonly string _apiToken;
    private string _userAccountId;
    private readonly AppUser _appUser;
    public JiraService(IConfiguration configuration, IUserService userService, AppUser appUser)
    {
        _configuration = configuration;
        _jiraBaseUrl = _configuration["Jira:BaseUrl"];
        _apiToken = _configuration["Jira:ApiToken"];
        _httpClient = new HttpClient();
        _appUser = appUser;
        var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_configuration["Jira:User"]}:{_apiToken}"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

        // Проверяем, существует ли пользователь
        Task.Run(async () => await EnsureUserExists()).Wait();
    }

    private async Task EnsureUserExists()
    {
        _userAccountId = await GetUserAccountIdAsync(_appUser.Email!);
        if (string.IsNullOrEmpty(_userAccountId))
        {
            _userAccountId = await CreateUserAsync(_appUser.Email, _appUser.Name);
        }
    }

    private async Task<string> GetUserAccountIdAsync(string email)
    {
        var response = await _httpClient.GetAsync($"{_jiraBaseUrl}/rest/api/3/user/search?query={Uri.EscapeDataString(email)}");
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        dynamic users = JsonConvert.DeserializeObject(responseBody);

        return users.Count > 0 ? users[0].accountId : null;
    }

    public async Task<string> CreateUserAsync(string email, string displayName)
    {
        var user = new
        {
            emailAddress = email,
            displayName = displayName,
            name = email.Split('@')[0],
            products = new[] { "jira-software" } // Укажите доступные продукты для пользователя
        };


        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_jiraBaseUrl}/rest/api/3/user", content);
        if (!response.IsSuccessStatusCode)
        {
            var responseBodyT = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error creating user: {response.StatusCode}, {responseBodyT}");
        }
        
        var responseBody = await response.Content.ReadAsStringAsync();
        dynamic createdUser = JsonConvert.DeserializeObject(responseBody);
        return createdUser.accountId;
        

        
    }

    public async Task<string> CreateIssueAsync(string description, string priority, string collection, string link)
    {
        var issue = new
        {
            fields = new
            {
                project = new { key = _configuration["Jira:ProjectKey"] },
                summary = "Collection: " + (collection ?? "null"),
                description = new
                {
                    version = 1,
                    type = "doc",
                    content = new[]
                    {
                        new
                        {
                            type = "paragraph",
                            content = new[]
                            {
                                new
                                {
                                    type = "text",
                                    text = description
                                }
                            }
                        }
                    }
                },
                issuetype = new { name = "Task" },
                priority = new { name = priority },
                customfield_10034 = link,
                reporter = new { id = _userAccountId } // Назначаем пользователя на задачу
            }
        };

        var json = JsonConvert.SerializeObject(issue);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync($"{_jiraBaseUrl}/rest/api/3/issue", content);
        
            var responseBody = await response.Content.ReadAsStringAsync();

            // Логирование запроса и ответа
            Console.WriteLine("Request JSON: " + json);
            Console.WriteLine("Response Status Code: " + response.StatusCode);
            Console.WriteLine("Response Body: " + responseBody);

            response.EnsureSuccessStatusCode();

            dynamic createdIssue = JsonConvert.DeserializeObject(responseBody);
            return createdIssue.key;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }
    
    public async Task<List<JiraIssue>> GetIssuesAsync()
    {
        var response = await _httpClient.GetAsync($"{_jiraBaseUrl}/rest/api/3/search?jql=reporter={_userAccountId}");

        if (!response.IsSuccessStatusCode)
        {
            var responseBodyT = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error fetching issues: {response.StatusCode}, {responseBodyT}");
        }

        var responseBody = await response.Content.ReadAsStringAsync();
        dynamic issuesResponse = JsonConvert.DeserializeObject(responseBody);

        var issues = new List<JiraIssue>();

        foreach (var issue in issuesResponse.issues)
        {
            var issueKey = issue.key.ToString();
            var issueLink = $"{_jiraBaseUrl}/browse/{issueKey}";

            issues.Add(new JiraIssue
            {
                Link = issueLink,
                Status = Enum.TryParse<JiraStatus>(issue.fields.status.name.ToString(), out JiraStatus status) ? status : JiraStatus.ToDo
            });
        }

        return issues;
    }

}