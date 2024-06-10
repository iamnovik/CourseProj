using CourseProj.Models;

namespace CourseProj.Repositories.Interfaces;

public interface IApiTokenRepository
{
    Task<ApiToken> AddTokenAsync(ApiToken token);
    Task<ApiToken?> GetApiTokenByUserId(string userId);

    Task<ApiToken?> GetApiTokenByToken(string token);
    
}