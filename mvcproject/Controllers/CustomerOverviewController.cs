using mvcproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace mvcproject.Controllers
{
    public class CustomerOverviewController : Controller
    {
        private customerEntities db = new customerEntities();

        // GET: CustomerOverview
        public ActionResult Index(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return View(db.vw_customerlists);
            }

            var vw = db.vw_customerlists.Where(co => co.客戶名稱.Contains(search));

            if (vw.Count() == 0)
            {
                ViewBag.Message = "無此客戶資訊 - " + search;
                return View();
            }

            return View(vw);
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vw_customerlists 客戶資訊 = db.vw_customerlists.Find(id);

            if (客戶資訊 == null)
            {
                return HttpNotFound();
            }
            //var vw = db.vw_customerlists.Where(co => co.客戶名稱.Contains(id));
            return View(客戶資訊);
        }

        //public ActionResult RedirectToCustomer(string name)
        //{
        //    return RedirectToAction("Index", "Customers", new { search = name});
        //    //return View();
        //}

        //public ActionResult RedirectToContactor(string name)
        //{
        //    return RedirectToAction("Index", "Contactors", new { search = name });
        //    //return View();
        //}
        //public ActionResult RedirectToBank(string name)
        //{
        //    return RedirectToAction("Index", "Banks", new { search = name });
        //    //return View();
        //}
    }
}