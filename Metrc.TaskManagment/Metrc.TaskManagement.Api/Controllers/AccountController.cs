using Metrc.TaskManagement.Application.Contracts.Infrastructure;
using Metrc.TaskManagement.Application.Contracts.Persistence;
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

    public AccountController(IUserService userService, IPasswordHasherService passwordHasherService)
    {
        _userService = userService;
        _passwordHasherService = passwordHasherService; 
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] CreateUserDTO createUserDTO)
    {
        var userDTO = await _userService.UserByEmailAsync(createUserDTO.Email);

        if (userDTO is not not null) return BadRequest("User with email already exists");

        var appUser = new AppUser
        {
            UserName = createUserDTO.UserName,
            Email = createUserDTO.Email,
            Password = _passwordHasherService.HashPassword(createUserDTO.Password)
        };

        var userId = await _userService.AddAsync(appUser);

        return Ok(userId);
    }


}
