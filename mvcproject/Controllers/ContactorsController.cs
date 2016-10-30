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
        private customerEntities db = new customerEntities();

        // GET: Contactors
        public ActionResult Index(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                var 客戶聯絡人 = db.客戶聯絡人.Where(c => c.isDeleted != true).Include(客 => 客.客戶資料);
                return View(客戶聯絡人.ToList());
            }

            var contactor = db.客戶聯絡人.Where(c => c.姓名.Contains(search) && c.isDeleted != true);
            var vwcontactor = db.客戶聯絡人.Where(c => c.isDeleted != true).Include(客 => 客.客戶資料);
            vwcontactor = vwcontactor.Where(v => v.客戶資料.客戶名稱.Contains(search));

            if (contactor.Count() == 0 && vwcontactor.Count() == 0)
            {
                ViewBag.Message = "查無此聯絡人資料";
                return View();
            } else if (vwcontactor.Count() > 0 && contactor.Count() == 0)
            {
                return View(vwcontactor);
            } else
            {
                return View(contactor);
            }
        }

        // GET: Contactors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: Contactors/Create
        public ActionResult Create()
        {

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
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
                var contactor = 客戶聯絡人;
                var allcontactors = db.客戶聯絡人;

                if (!contactor.isValidContactorEmail(allcontactors))
                {
                    TempData["status"] = "此客戶聯絡人 Email 不能重複，請重新輸入";
                    return RedirectToAction("Create");
                }

                db.客戶聯絡人.Add(客戶聯絡人);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: Contactors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: Contactors/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,isDeleted")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: Contactors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: Contactors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            //db.客戶聯絡人.Remove(客戶聯絡人);
            客戶聯絡人.isDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
