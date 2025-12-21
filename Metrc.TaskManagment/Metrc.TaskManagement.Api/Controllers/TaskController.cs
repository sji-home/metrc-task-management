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
    public async Task<IActionResult> GetAll()
    {
        var result = await _workTaskService.GetAll();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var result = await _workTaskService.GetWorkTask(id);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody] WorkTask workTask)
    {
        var result = await _workTaskService.CreateTask(workTask);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask([FromBody] WorkTask workTask)
    {
        var result = await _workTaskService.UpdateWorkTask(workTask);

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var result = await _workTaskService.DeleteWorkTask(id);

        return Ok(result);
    }
}
