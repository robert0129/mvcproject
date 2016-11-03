using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcproject.Models;

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
            var info = filterContext.DisplayMode;
            base.OnActionExecuting(filterContext);
        }
    }
}