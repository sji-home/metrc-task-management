using Metrc.TaskManagement.Application.Common;
using Metrc.TaskManagement.Application.Contracts.Persistence;
using Metrc.TaskManagement.Domain.DTOs;
using Metrc.TaskManagement.Domain.Entities;
using Metrc.TaskManagment.Domain.Enums;

namespace Metrc.TaskManagement.Persistence.Services;

public class UserService : IUserService
{
    private readonly IDbService _dbService;

    public UserService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<int> AddAsync(AppUser appUser, CancellationToken cancellationToken = default)
    {
        var sql = @"INSERT INTO public.app_user (username, email, password) 
                    VALUES (@UserName, @Email, @Password) RETURNING id";

        var id = await _dbService.GetAsync<int>(sql, appUser, cancellationToken);
        return id;
    }

    public async Task<UserDTO?> UserByLoginIdAsync(string loginId, CancellationToken cancellationToken = default)
    {
        var sql1 = @"select id, username, email, password 
                    from public.app_user 
                    where username=@loginId";

        var user = await _dbService.GetAsync<AppUser>(sql1, new { loginId }, cancellationToken);

        if (user is null)
            return null;

        var userDTO = new UserDTO
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Password = user.Password
        };

        var sql2 = @"
            SELECT 
                ur.app_user_id AS AppUserId,
                r.role_name   AS RoleName
            FROM public.app_user_role ur
            JOIN public.app_role r
                ON ur.app_role_id = r.id
            WHERE ur.app_user_id = @Id;";

        var userRoles = await _dbService.GetListAsync<AppUserRole>(sql2, new { userDTO.Id }, cancellationToken);

        if (userRoles?.Count > 0)
        {
            var appUserRoleDTOs = userRoles
                .Select(r => new AppUserRoleDTO
                {
                    AppUserId = r.AppUserId,
                    RoleName = r.RoleName
                })
                .ToList();

            userDTO.AppUserRoles.AddRange(appUserRoleDTOs);
        }

        return userDTO;
    }

    public async Task<Result> AddRolesAsync(int userId, IReadOnlyCollection<RoleEnum> roles, CancellationToken cancellationToken = default)
    {
        if (userId <= 0)
        {
            return await Task.FromResult(Result.Fail("The userId must be greater than zero."));
        }

        if (roles?.Count == 0)
        {
            return await Task.FromResult(Result.Fail("There were no roles passed in."));
        }

        const string sql = """
            INSERT INTO app_user_role (app_user_id, app_role_id)
            VALUES (@UserId, @RoleId)
            ON CONFLICT DO NOTHING;
        """;

        var parameters = roles!
            .Where(r => r != RoleEnum.None)
            .Select(r => new
            {
                UserId = userId,
                RoleId = (int)r
            });

        var rows = await _dbService.EditDataAsync(sql, parameters, cancellationToken);

        if (rows == 0)
        {
            return Result.Fail($"No roles were added for UserId = {userId}.");
        }

        return Result.Ok();
    }
}
