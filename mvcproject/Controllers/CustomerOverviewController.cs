using mvcproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace mvcproject.Controllers
{
    public class CustomerOverviewController : BaseController
    {
        vw_customerlistsRepository repo = RepositoryHelper.Getvw_customerlistsRepository();
        // GET: CustomerOverview
        public ActionResult Index(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return View(repo.All());
            }

            var vw = repo.All().Where(c => c.客戶名稱.Contains(search));

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

            var vw = repo.All().FirstOrDefault(c => c.客戶名稱 == id);

            if (vw == null)
            {
                return HttpNotFound();
            }

            return View(vw);
        }

        public JsonResult GetTotalInfo()
        {   
            repo.UnitOfWork.LazyLoadingEnabled = false;
            var data = repo.All().OrderBy(p => p.客戶名稱).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}