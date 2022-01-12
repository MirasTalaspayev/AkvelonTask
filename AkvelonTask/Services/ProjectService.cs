using AkvelonTask.Data;
using AkvelonTask.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AkvelonTask.Services
{
    public class ProjectService : BaseService<Project>, IProjectService
    {
        public ProjectService(AppDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<IEnumerable<Project>> GetAll()
        {
            return await _dbContext.Projects.Where(x => !x.IsDeleted).Include(x => x.Tasks.Where(task => !task.IsDeleted)).ToListAsync();
        }
        public override async Task<Project> Get(int id)
        {
            await EntityExist(id);
            return await _dbContext.Projects.Include(x => x.Tasks.Where(t => !t.IsDeleted)).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddTask(int ProjectId, TaskInfo task)
        {
            var project = await Get(ProjectId);
            project.Tasks.Add(task);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<TaskInfo>> GetTasks(int projectId)
        {
            return (await Get(projectId)).Tasks;
        }
        public override async Task<Project> EntityExist(int id)
        {
            var project = await _dbContext.Projects.Include(p => p.Tasks.Where(t => !t.IsDeleted)).FirstOrDefaultAsync(p => p.Id == id);
            if (project == null)
            {
                ThrowDoesNotExistException();
            }
            return project;
        }
    }
}
