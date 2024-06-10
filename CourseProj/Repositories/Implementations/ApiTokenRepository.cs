using CourseProj.Data;
using CourseProj.Models;
using CourseProj.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseProj.Repositories.Implementations;

public class ApiTokenRepository(AppDbContext appDbContext) : IApiTokenRepository
{
    public async Task<ApiToken> AddTokenAsync(ApiToken token)
    {
        appDbContext.ApiTokens.Add(token);
        await appDbContext.SaveChangesAsync();
        return token;
    }

    public async Task<ApiToken?> GetApiTokenByUserId(string userId)
    {
        var token = await appDbContext.ApiTokens.FirstOrDefaultAsync(t => t.UserId == userId);

        return token;
    }

    public async Task<ApiToken?> GetApiTokenByToken(string token)
    {
        var apiToken = await appDbContext.ApiTokens.FirstOrDefaultAsync(t => t.Token == token);

        return apiToken;
    }

}