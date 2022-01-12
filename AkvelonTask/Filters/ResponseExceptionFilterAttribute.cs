using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AkvelonTask.Filters
{
    /// <summary>
    /// Custom Exception Filter that returns Json with StatusCode.
    /// </summary>
    public class ResponseExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var error = new // Json
            {
                Success = false,
                Error = context.Exception.Message
            };
            context.Result = new ObjectResult(error)
            {
                StatusCode = (int?)context.Exception.Data["StatusCode"]
            };
            context.ExceptionHandled = true;
        }
    }
}
