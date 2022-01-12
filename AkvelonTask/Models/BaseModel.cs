using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AkvelonTask.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        /// <summary>
        /// We do not delete items, we hide them
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
