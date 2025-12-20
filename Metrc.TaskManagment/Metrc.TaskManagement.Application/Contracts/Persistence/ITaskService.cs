using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrc.TaskManagement.Application.Contracts.Persistence;

public interface ITaskService
{
    Task<int> CreateTask(Task task);
    Task<List<Task>> GetAll();
    //Task<List<Task>> GetTasksForUser();
    Task<Task> GetTask(int id);
    Task<bool> UpdateTask(Task task);
    Task<bool> DeleteTask(int key);
}
