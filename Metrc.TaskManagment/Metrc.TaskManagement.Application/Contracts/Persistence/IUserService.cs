using Metrc.TaskManagement.Application.Common;
using Metrc.TaskManagement.Domain.DTOs;
using Metrc.TaskManagement.Domain.Entities;
using Metrc.TaskManagment.Domain.Enums;

namespace Metrc.TaskManagement.Application.Contracts.Persistence;

public interface IUserService
{
    Task<UserDTO?> UserByLoginIdAsync(string loginId, CancellationToken cancellationToken = default);

    Task<int> AddAsync(AppUser appUser, CancellationToken cancellationToken = default);

    Task<Result> AddRolesAsync(int userId, IReadOnlyCollection<RoleEnum> roles, CancellationToken cancellationToken = default);
}
