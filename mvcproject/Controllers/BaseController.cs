using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcproject.Models;
using System.Diagnostics;

namespace mvcproject.Controllers
{
    public abstract class BaseController : Controller
    {
        protected customerEntities db = new customerEntities();

        protected override void HandleUnknownAction(string actionName)
        {
            this.RedirectToAction("Index").ExecuteResult(this.ControllerContext);
            //base.HandleUnknownAction(actionName);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Time = DateTime.Now.ToFileTimeUtc().ToString();
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string currentTime = DateTime.Now.ToFileTimeUtc().ToString();
            string startTime = ViewBag.Time;
            long result = Convert.ToInt64(currentTime) - Convert.ToInt64(startTime);
            Trace.TraceError("Duration : {0} seconds", (result * (1E-09)));
            base.OnActionExecuted(filterContext);
        }
    }
}