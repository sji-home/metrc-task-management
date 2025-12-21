using Metrc.TaskManagement.Application.Contracts.Persistence;
using Metrc.TaskManagement.Domain.DTOs;
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

    public async Task<UserDTO> UserByEmailAsync(string email)
    {
        var sql1 = @"select id, username, email, password 
                    from public.app_user 
                    where email=@email";

        var user = await _dbService.GetAsync<AppUser>(sql1, new { email });
        var userDTO = new UserDTO
        {
            UserName = user.UserName,
            Email = user.Email,
            Password = user.Password
        };

        var sql2 = @"select * 
                     from public.app_user_role ur
                     join public.role r
                     on ur.role_id = r.id
                     where ur.app_user_id=@user_id";                     


        var userRoles = await _dbService.GetList<AppUserRole>(sql2, new { user.Id });

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

        return userDTO;
    }
}
