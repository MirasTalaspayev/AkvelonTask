using AkvelonTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AkvelonTask.Services
{
    public interface IProjectService : IBaseService<Project>
    {
        public Task AddTask(int ProjectId, TaskInfo task);
        public Task<IEnumerable<TaskInfo>> GetTasks(int projectId);
    }
}
