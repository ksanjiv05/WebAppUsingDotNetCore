using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger logger;

        public ErrorController(ILogger logger)
        {
            this.logger = logger;
        }
        [Route("Error")]
        public IActionResult Error()
        {
            var httpContext = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.ErrorMessage = "Sorry : i Don't process your request "+httpContext.Error.Message+" Path :"+httpContext.Path;
            logger.LogWarning("Warrning Logging : --- " + httpContext.Error.Message + " Path: " + httpContext.Path + " " + httpContext.Error.StackTrace);
            return View("Error");
        }
    }
}