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

    public async Task<int> AddAsync(AppUser appUser)
    {
        var sql = @"INSERT INTO public.app_user (username, email, password) 
                    VALUES (@UserName, @Email, @Password) RETURNING id";

        var id = await _dbService.GetAsync<int>(sql, appUser);
        return id;
    }

    public async Task<UserDTO?> UserByLoginIdAsync(string loginId)
    {
        var sql1 = @"select id, username, email, password 
                    from public.app_user 
                    where username=@loginId";

        var user = await _dbService.GetAsync<AppUser>(sql1, new { loginId });

        if (user is null)
            return null;

        var userDTO = new UserDTO
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Password = user.Password
        };

        var sql2 = @"select * 
                     from public.app_user_role ur
                     join public.role r
                     on ur.role_id = r.id
                     where ur.app_user_id=@Id";                     


        var userRoles = await _dbService.GetList<AppUserRole>(sql2, new { userDTO.Id });

        if (userRoles?.Count > 0)
        {
            var userRoleDTOs = userRoles

            .Select(r => new UserRoleDTO
            {
                AppUserId = r.AppUserId,
                Role = new RoleDTO
                {
                    Name = r.Role.Name
                }
            }).ToList();

            userDTO.AppUserRoles.AddRange(userRoleDTOs);
        }

        return userDTO;
    }

    public async Task<Result> AddRolesAsync(int userId, IReadOnlyCollection<RoleEnum> roles)
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
            INSERT INTO app_user_role (app_user_id, role_id)
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

        var rows = await _dbService.EditData(sql, parameters);

        if (rows == 0)
        {
            return Result.Fail($"No roles were added for UserId = {userId}.");
        }

        return Result.Ok();
    }
}
