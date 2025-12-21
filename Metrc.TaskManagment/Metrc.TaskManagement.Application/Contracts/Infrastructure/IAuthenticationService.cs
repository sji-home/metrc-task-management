using Metrc.TaskManagement.Application.DTOs.Authentication;

namespace Metrc.TaskManagement.Application.Contracts.Infrastructure;

public interface IAuthenticationService
{
    Task<TokenResponse?> CreateAccessTokenAsync(
        string username,
        string password, 
        CancellationToken cancellationToken);
}
