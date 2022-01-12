using AkvelonTask.Models;
using AkvelonTask.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AkvelonTask.Filters;
using AkvelonTask.Enums;
using Microsoft.EntityFrameworkCore;

namespace AkvelonTask.Controllers
{
    /// <summary>
    /// Controller for Projects Table.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private IProjectService Service { get; }
        public ProjectsController(IProjectService service)
        {
            Service = service;
        }
        /// <summary>
        /// Returns all Projects
        /// </summary>
        /// <returns>Projects</returns>
        [HttpGet]
        public async Task<IEnumerable<Project>> GetAll()
        {
            return await FilterData();
        }
        /// <summary>
        /// Returns a Project according to given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Project</returns>
        [HttpGet("{id}")]
        public async Task<Project> GetById([FromRoute]int id)
        {
            return await Service.Get(id);
        }
        /// <summary>
        /// Adds a new Project
        /// </summary>
        /// <param name="project"></param>
        /// <returns>StatusCode</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Project project)
        {
            await Service.Add(project);
            return StatusCode(201);
        }
        /// <summary>
        /// Adds a Task to Project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="task"></param>
        /// <returns>StatusCode</returns>
        [HttpPost("{projectId}")]
        public async Task<IActionResult> AddTask([FromRoute]int projectId, [FromBody] TaskInfo task)
        {
            await Service.AddTask(projectId, task);
            return StatusCode(201);
        }
        /// <summary>
        /// Updates a Project if successful, exception message otherwise.
        /// </summary>
        /// <param name="project"></param>
        /// <returns>StatusCode</returns>
        [HttpPut]
        public async Task<StatusCodeResult> Update([FromBody]Project project)
        {
            await Service.Update(project);
            return StatusCode(201);
        }
        /// <summary>
        /// Deletes(Hide) the Project if successful, exception message otherwise
        /// </summary>
        /// <param name="id"></param>
        /// <returns>StatusCode</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Service.Delete(id);
            return StatusCode(200);
        }
        /// <summary>
        /// Filters the Projects according to sortBy and field values
        /// </summary>
        /// <returns>Projects</returns>
        private async Task<IEnumerable<Project>> FilterData()
        {
            return await ProjectsAfterSortAsync();
        }
        private async Task<IEnumerable<Project>> ProjectsByFields()
        {
            var result = await Service.GetAll();
            var query = HttpContext.Request.Query;

            int status = -1;
            int priority = -1;
            DateTime start_date = default;
            DateTime end_date = default;
            #region // try catch blocks
            try
            {
                status = query.ContainsKey("status") ? Convert.ToInt32(query["status"]) : -1;               
            }
            catch { }
            try
            {
                priority = query.ContainsKey("priority") ? Convert.ToInt32(query["priority"]) : -1;
            }
            catch { }
            try
            {
                start_date = query.ContainsKey("start_date") ? Convert.ToDateTime(query["start_date"]) : default;
            }
            catch { }
            try
            {
                end_date = query.ContainsKey("end_date") ? Convert.ToDateTime(query["end_date"]) : default;
            }
            catch { }
            #endregion
            if (status >= 0)
            {
                result = result.Where(prj => prj.Status == (ProjectStatus)status);
            }
            if (priority != -1)
            {
                result = result.Where(prj => prj.Priority == priority);
            }
            if (start_date != default)
            {
                result = result.Where(prj => prj.StartDate == start_date);
            }
            if (end_date != default)
            {
                result = result.Where(prj => prj.EndDate == end_date);
            }
            return result;
        }
        private async Task<IEnumerable<Project>> ProjectsAfterSortAsync()
        {
            IEnumerable<Project> projects = await ProjectsByFields();
     
            var query = HttpContext.Request.Query;

            string sortBy = query["sortBy"];
            string order = query["order"];

            switch (sortBy)
            {
                case "start_date":
                    projects = order == "asc" ? projects.OrderBy(entity => entity.StartDate) : projects.OrderByDescending(entity => entity.StartDate);
                    break;
                case "end_date":
                    projects = order == "asc" ? projects.OrderBy(entity => entity.EndDate) : projects.OrderByDescending(entity => entity.EndDate);
                    break;
                case "priority":
                    projects = order == "asc" ? projects.OrderBy(entity => entity.Priority) : projects.OrderByDescending(entity => entity.Priority);
                    break;
            }

            return projects;
        }

    }
}
