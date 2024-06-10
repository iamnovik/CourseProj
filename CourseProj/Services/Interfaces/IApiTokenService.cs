using CourseProj.Models;

namespace CourseProj.Services.Interfaces;

public interface IApiTokenService
{
    Task<ApiToken> AddTokenAsync(string userId);

    Task<ApiToken?> GetApiTokenByUserId(string userId);

    Task<ApiToken?> GetApiTokenByToken(string token);
    
}