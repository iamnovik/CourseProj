using CourseProj.Models;
using CourseProj.Repositories.Interfaces;
using CourseProj.Services.Interfaces;

namespace CourseProj.Services.Implementations;

public class ApiService(IApiTokenRepository apiTokenRepository) : IApiTokenService
{
    public async Task<ApiToken> AddTokenAsync(string userId)
    {
        var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        var apiToken = new ApiToken
        {
            Token = token,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };
        await apiTokenRepository.AddTokenAsync(apiToken);
        return apiToken;
    }

    public async Task<ApiToken?> GetApiTokenByUserId(string userId)
    {
        var apiToken = await apiTokenRepository.GetApiTokenByUserId(userId);

        return apiToken;
    }

    public async Task<ApiToken?> GetApiTokenByToken(string token)
    {
        var apiToken = await apiTokenRepository.GetApiTokenByToken(token);

        return apiToken;

    }
}