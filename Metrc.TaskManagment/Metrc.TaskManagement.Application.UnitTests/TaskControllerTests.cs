using Metrc.TaskManagement.Api.Controllers;
using Metrc.TaskManagement.Application.Contracts.Persistence;
using Metrc.TaskManagement.Domain.DTOs;
using Metrc.TaskManagment.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Metrc.TaskManagement.Application.UnitTests;

public class TaskControllerTests
{
    private readonly Mock<IWorkTaskService> _mockService;
    private readonly TaskController _controller;

    public TaskControllerTests()
    {
        _mockService = new Mock<IWorkTaskService>();
        _controller = new TaskController(_mockService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkWithTasks()
    {
        // Arrange
        var tasks = new List<WorkTaskResponseDTO>
        {
            new WorkTaskResponseDTO { Id = 1, Title = "Task1", Status = WorkTaskStatusEnum.Pending },
            new WorkTaskResponseDTO { Id = 2, Title = "Task2", Status = WorkTaskStatusEnum.Completed }
        };

        _mockService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(tasks);

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTasks = Assert.IsAssignableFrom<IEnumerable<WorkTaskResponseDTO>>(okResult.Value);
        Assert.Equal(2, returnedTasks.Count());
    }

}
