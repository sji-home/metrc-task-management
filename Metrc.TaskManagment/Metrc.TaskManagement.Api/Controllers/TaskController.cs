using Metrc.TaskManagement.Application.Contracts.Persistence;
using Metrc.TaskManagement.Domain.Entities;
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

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var result = await _workTaskService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTask(
        int id, 
        CancellationToken cancellationToken = default)
    {
        var result = await _workTaskService.GetWorkTaskAsync(id);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddTask(
        [FromBody] WorkTask workTask, 
        CancellationToken cancellationToken = default)
    {
        var result = await _workTaskService.CreateTaskAsync(workTask);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask(
        [FromBody] WorkTask workTask, 
        CancellationToken cancellationToken = default)
    {
        var result = await _workTaskService.UpdateWorkTaskAsync(workTask);

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask(
        int id, 
        CancellationToken cancellationToken = default)
    {
        var result = await _workTaskService.DeleteWorkTaskAsync(id);

        return Ok(result);
    }
}
