using Metrc.TaskManagement.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Metrc.TaskManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _taskService.GetAll();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var result = await _taskService.GetTask(id);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody] Task task)
    {
        var result = await _taskService.CreateTask(task);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask([FromBody] Task task)
    {
        var result = await _taskService.UpdateTask(task);

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var result = await _taskService.DeleteTask(id);

        return Ok(result);
    }
}
