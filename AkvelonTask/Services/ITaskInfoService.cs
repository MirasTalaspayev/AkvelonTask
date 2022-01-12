using AkvelonTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AkvelonTask.Services
{
    public interface ITaskInfoService : IBaseService<TaskInfo>
    {
        public Task<IEnumerable<TaskInfo>> GetByProjectIdAsync(int projectId);
    }
}
