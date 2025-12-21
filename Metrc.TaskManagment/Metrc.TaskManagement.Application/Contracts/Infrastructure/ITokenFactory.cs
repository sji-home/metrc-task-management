using Metrc.TaskManagement.Application.DTOs.Authentication;
using Metrc.TaskManagement.Domain.DTOs;

namespace Metrc.TaskManagement.Application.Contracts.Infrastructure;

public interface ITokenFactory
{
    TokenResponse CreateAccessToken(UserDTO user);
}
