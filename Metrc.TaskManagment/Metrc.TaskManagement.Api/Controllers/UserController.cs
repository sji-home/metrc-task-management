using Metrc.TaskManagement.Application.Contracts.Persistence;
using Metrc.TaskManagment.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Metrc.TaskManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = nameof(RoleEnum.Admin))]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("{userId}/roles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AddRolesToUser(
        int userId, 
        [FromBody] IReadOnlyCollection<RoleEnum> roles, 
        CancellationToken cancellationToken = default)
    {
        if (roles == null || !roles.Any())
            return BadRequest(new { Message = "At least one role must be provided." });

        var result = await _userService.AddRolesAsync(userId, roles, cancellationToken);

        if (result is not { IsSuccess: true }) 
            return BadRequest(result?.Error);

        return Ok("Roles added successfully.");
    }
}
