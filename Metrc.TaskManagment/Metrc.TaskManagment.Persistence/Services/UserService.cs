using Metrc.TaskManagement.Application.Contracts.Persistence;
using Metrc.TaskManagement.Domain.Entities;

namespace Metrc.TaskManagement.Persistence.Services;

public class UserService : IUserService
{
    private readonly IDbService _dbService;

    public UserService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<int> AddAsync(AppUser appUser)
    {
        var sql = @"INSERT INTO public.app_user (username, email, password) 
                    VALUES (@UserName, @Email, @Password) RETURNING id";

        var id = await _dbService.GetAsync<int>(sql, appUser);
        return id;
    }

    public async Task<int> UserByEmailAsync(string email)
    {
        var sql = @"select count(*) from public.app_user where email=@email";

        var cnt = await _dbService.GetAsync<int>(sql, new { email });
        return cnt;
    }
}
