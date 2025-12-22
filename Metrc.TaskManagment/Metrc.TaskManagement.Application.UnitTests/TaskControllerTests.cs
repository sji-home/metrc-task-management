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
        var task1 = new WorkTaskResponseDTO
        {
            Id = 1,
            Title = "Task1",
            Description = "Description",
            Status = WorkTaskStatusEnum.Pending
        };


        var task2 = new WorkTaskResponseDTO
        {
            Id = 2,
            Title = "Task2",
            Description = "Description",
            Status = WorkTaskStatusEnum.Completed
        };

        var tasks = new List<WorkTaskResponseDTO>();
        tasks.Add(task1);
        tasks.Add(task2);

        _mockService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(tasks);

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTasks = Assert.IsAssignableFrom<IEnumerable<WorkTaskResponseDTO>>(okResult.Value);
        Assert.Equal(2, returnedTasks.Count());
    }

    [Fact]
    public async Task Get_TaskExists_ReturnsOk()
    {
        // Arrange
        var task = new WorkTaskResponseDTO
        {
            Id = 1,
            Title = "Task1",
            Description = "Description",
            Status = WorkTaskStatusEnum.Pending
        };

        _mockService.Setup(s => s.GetWorkTaskAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(task);

        // Act
        var result = await _controller.Get(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTask = Assert.IsType<WorkTaskResponseDTO>(okResult.Value);
        Assert.Equal(1, returnedTask.Id);
    }

    


    [Fact]
    public async Task Delete_Success_ReturnsNoContent()
    {
        // Arrange
        _mockService.Setup(s => s.DeleteWorkTaskAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }


}