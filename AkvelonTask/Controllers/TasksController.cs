using AkvelonTask.Filters;
using AkvelonTask.Models;
using AkvelonTask.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AkvelonTask.Controllers
{
    /// <summary>
    /// Task Controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private ITaskInfoService Service { get; }
        public TasksController(ITaskInfoService service)
        {
            Service = service;
        }
        /// <summary>
        /// Returns All tasks
        /// </summary>
        /// <returns>TaskInfos</returns>
        [HttpGet]
        public async Task<IEnumerable<TaskInfo>> GetAll()
        {
            return await Service.GetAll();
        }
        /// <summary>
        /// Return
        /// </summary>
        /// <param name="id"></param>
        /// <returns>TaskInfo</returns>
        [HttpGet("{id}"), ResponseExceptionFilter]
        public async Task<TaskInfo> Get([FromRoute]int id)
        {
            return await Service.Get(id);
        }
        /// <summary>
        /// Updates a TaskInfo. Returns StatusCode 201 if successful, Exception Message otherwise.
        /// </summary>
        /// <param name="task"></param>
        /// <returns>StatusCode</returns>
        [HttpPut]
        public async Task<IActionResult> Update(TaskInfo task)
        {
            await Service.Update(task);
            return StatusCode(201);
        }
        /// <summary>
        /// Deletes a TaskInfo. Returns StatusCode 200 if successful, Exception Message otherwise.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>StatusCode</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Service.Delete(id);
            return StatusCode(200);
        }
    }
}
