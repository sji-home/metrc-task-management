using Metrc.TaskManagement.Application.Contracts.Infrastructure;
using Metrc.TaskManagement.Application.Contracts.Persistence;
using Metrc.TaskManagement.Application.DTOs.Authentication;
using System.Net;

namespace Metrc.TaskManagement.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserService _userService;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly ITokenFactory _tokenFactory;

    public AuthenticationService(
        IUserService userService,
        IPasswordHasherService passwordHasherService,
        ITokenFactory tokenFactory)
    {
        _userService = userService;
        _passwordHasherService = passwordHasherService;
        _tokenFactory = tokenFactory;
    }

    public async Task<TokenResponse?> CreateAccessTokenAsync(
        string username,
        string password,
        CancellationToken cancellationToken = default)
    {
        var user = await _userService.UserByLoginIdAsync(username);

        if (user is null)
            return null;

        var passwordValid =
            _passwordHasherService.PasswordMatches(password, user.Password);

        if (!passwordValid)
            return null;

        var tokenResponse = _tokenFactory.CreateAccessToken(user);

        return tokenResponse;
    }
}
