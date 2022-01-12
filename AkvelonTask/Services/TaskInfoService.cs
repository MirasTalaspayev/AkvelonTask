using AkvelonTask.Data;
using AkvelonTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AkvelonTask.Services
{
    public class TaskInfoService : BaseService<TaskInfo>, ITaskInfoService
    {
        public TaskInfoService(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<TaskInfo>> GetByProjectIdAsync(int projectId)
        {
            var project = await _dbContext.Projects.FindAsync(projectId);
            if (project == null)
            {
                throw new Exception($"Project with Id: {projectId} does not exist");
            }
            return project.Tasks;
        }
    }
}
