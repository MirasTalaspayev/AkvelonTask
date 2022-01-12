using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AkvelonTask.Models
{
    public class TaskInfo : BaseModel
    {
        /// <summary>
        /// Task Name
        /// </summary>
        [StringLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// Task Description
        /// </summary>
        [StringLength(250)]
        public string Description { get; set; }
        /// <summary>
        /// many-to-one relation
        /// </summary>
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        /// <summary>
        /// 0 - ToDo;
        /// 1 - InProcess;
        /// 2 - Done
        /// </summary>
        public TaskStatus Status { get; set; }
    }
}
