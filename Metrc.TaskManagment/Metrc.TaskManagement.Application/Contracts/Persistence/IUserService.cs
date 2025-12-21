using Metrc.TaskManagement.Domain.DTOs;
using Metrc.TaskManagement.Domain.Entities;

namespace Metrc.TaskManagement.Application.Contracts.Persistence;

public interface IUserService
{
    Task<int> UserByEmailAsync(string email);

    Task<int> AddAsync(AppUser appUser);
}
