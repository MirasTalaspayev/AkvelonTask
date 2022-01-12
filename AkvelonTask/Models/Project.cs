using AkvelonTask.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AkvelonTask.Models
{
    public class Project : BaseModel
    {
        private DateTime startDate;
        private DateTime endDate;
        /// <summary>
        /// Name for Project
        /// </summary>
        [StringLength(150)]
        public string Name { get; set; }
        /// <summary>
        /// Start Date of a Project
        /// </summary>
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/dd/yyyy}")]
        [Range(typeof(DateTime), "2022-1-1", "2100-1-1", ErrorMessage = "Date should be between 2022 and 2100.")]
        public DateTime StartDate { get { return startDate; } set { startDate = value; } }
        /// <summary>
        /// End Date of a project. 
        /// Validates if EndDate is after StartDate.
        /// </summary>
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/dd/yyyy}")]
        [Range(typeof(DateTime), "2022-1-1", "2100-1-1", ErrorMessage = "Date should be between 2022 and 2100.")]
        public DateTime EndDate 
        { 
            get { return endDate; } 
            set 
            {
                if (value < startDate)
                {
                    var exception = new ArgumentException("EndDate Should be after StartDate");
                    exception.Data["StatusCode"] = 400;
                    throw exception;
                }
                endDate = value;
            } 
        }
        [Range(1, int.MaxValue, ErrorMessage = "Priority should be a positive number.")]
        public int Priority { get; set; }
        /// <summary>
        /// 0 - NotStarted,
        /// 1 - Active,
        /// 2 - Completed
        /// </summary>
        [Range(0, 2, ErrorMessage = "ProjectStatus should be in range [0, 2].")]
        public ProjectStatus Status { get; set; }
        /// <summary>
        /// many-to-one relation: 
        /// One project may have several tasks.
        /// </summary>
        public ICollection<TaskInfo> Tasks { get; set; }
    }
}
