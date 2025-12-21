using Metrc.TaskManagement.Application.Contracts.Infrastructure;
using Metrc.TaskManagement.Application.Contracts.Persistence;
using Metrc.TaskManagement.Application.DTOs.Authentication;
using Metrc.TaskManagement.Domain.DTOs;
using Metrc.TaskManagement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Metrc.TaskManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly IAuthenticationService _authenticationService;

    public AccountController(
        IUserService userService, 
        IPasswordHasherService passwordHasherService,
        IAuthenticationService authenticationService)
    {
        _userService = userService;
        _passwordHasherService = passwordHasherService;
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Register([FromBody] CreateUserDTO createUserDTO)
    {
        var userDTO = await _userService.UserByLoginIdAsync(createUserDTO.UserName);

        if (userDTO is not null) return Unauthorized("Invalid credentials.");

        var appUser = new AppUser
        {
            UserName = createUserDTO.UserName,
            Email = createUserDTO.Email,
            Password = _passwordHasherService.HashPassword(createUserDTO.Password)
        };

        var userId = await _userService.AddAsync(appUser);

        return Ok(userId);
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TokenResponse>> Login(
        LoginDTO request,
        CancellationToken cancellationToken = default)
    {
        var tokenResponse = await _authenticationService.CreateAccessTokenAsync(
            request.Email,
            request.Password, 
            cancellationToken);

        if (tokenResponse is null)
            return Unauthorized("Invalid credentials.");

        return Ok(tokenResponse);
    }

}
