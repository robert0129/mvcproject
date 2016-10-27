using mvcproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvcproject.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            UserModel user = new UserModel();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel user)
        {
            if (string.Equals(user.name, "admin") && string.Equals(user.password, "admin"))
            {
                return RedirectToAction("Index", "CustomerOverview");
            }
            return View("Login");
        }
    }


}