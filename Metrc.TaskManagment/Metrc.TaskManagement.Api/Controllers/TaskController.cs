using Metrc.TaskManagement.Application.Contracts.Persistence;
using Metrc.TaskManagement.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Metrc.TaskManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly IWorkTaskService _workTaskService;

    public TaskController(IWorkTaskService taskService)
    {
        _workTaskService = taskService;
    }

    [HttpGet()]
    [ProducesResponseType(typeof(IEnumerable<WorkTaskResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
    {
        var result = await _workTaskService.GetAllAsync(cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(WorkTaskResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(
        int id, 
        CancellationToken cancellationToken = default)
    {
        var task = await _workTaskService.GetWorkTaskAsync(id, cancellationToken);

        return task is not null
            ? Ok(task)
            : NotFound(new { Message = $"WorkTask with Id {id} was not found." });
    }

    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> Add(
        [FromBody] WorkTaskDTO workTaskDTO, 
        CancellationToken cancellationToken = default)
    {
        var result = await _workTaskService.CreateTaskAsync(workTaskDTO, cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id:int}")]
    [Consumes("application/json")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateWorkTaskDTO updateWorkTaskDTO, 
        CancellationToken cancellationToken = default)
    {
        if (id != updateWorkTaskDTO.Id)
            return BadRequest(new { Message = "Id in route does not match Id in body." });

        var result = await _workTaskService.UpdateWorkTaskAsync(updateWorkTaskDTO, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id, 
        CancellationToken cancellationToken = default)
    {
        var deleted = await _workTaskService.DeleteWorkTaskAsync(id, cancellationToken);

        return deleted ? NoContent() : NotFound(new { Message = $"WorkTask with Id {id} not found." });
    }
}
