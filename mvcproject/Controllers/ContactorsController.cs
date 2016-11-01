using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvcproject.Models;

namespace mvcproject.Controllers
{
    public class ContactorsController : Controller
    {
        客戶聯絡人Repository repo = RepositoryHelper.Get客戶聯絡人Repository();
        客戶資料Repository clientRepo = RepositoryHelper.Get客戶資料Repository();

        // GET: Contactors
        public ActionResult Index(string search, string JobTitle)
        {
            var jtitle = repo.All().Select(c => c.職稱);
            ViewBag.JobTitle = new SelectList(jtitle);

            var contactors = repo.All(search, JobTitle);

            if (contactors.Count() == 0)
            {
                ViewBag.Message = "查無此聯絡人資料";
            }
            return View(contactors);
        }

        // GET: Contactors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contactor = repo.Find(id.Value);
            
            if (contactor == null)
            {
                return HttpNotFound();
            }
            return View(contactor);
        }

        // GET: Contactors/Create
        public ActionResult Create()
        {

            ViewBag.客戶Id = new SelectList(repo.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: Contactors/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, 客戶Id, 職稱, 姓名, Email, 手機, 電話, isDeleted")] 客戶聯絡人 客戶聯絡人)
        { 
            if (ModelState.IsValid)
            {
                repo.Add(客戶聯絡人);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(clientRepo.All(), "Id", "客戶名稱",  客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: Contactors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contactor = repo.Find(id.Value);
            if (contactor == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(clientRepo.All(), "Id", "客戶名稱", contactor.客戶Id);
            return View(contactor);
        }

        // POST: Contactors/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {

            var contactor = repo.Find(id);

            if (TryUpdateModel(contactor))
            {
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(clientRepo.All(), "Id", "客戶名稱", contactor.客戶Id);
            return View(contactor);
        }

        // GET: Contactors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contactor = repo.Find(id.Value);
            if (contactor == null)
            {
                return HttpNotFound();
            }
            return View(contactor);
        }

        // POST: Contactors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var contactor = repo.Find(id);
            repo.Delete(contactor);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
