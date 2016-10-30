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
    public class BanksController : Controller
    {
        //private customerEntities db = new customerEntities();

        客戶銀行資訊Repository repo = RepositoryHelper.Get客戶銀行資訊Repository();
        客戶資料Repository clientRepo = RepositoryHelper.Get客戶資料Repository();
        // GET: Banks
        public ActionResult Index(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return View(repo.All().ToList());
            }

            var banks = repo.All().Where(b => b.銀行名稱.Contains(search));
            var vwbanks = repo.All().Where(b => b.客戶資料.客戶名稱.Contains(search));

            if (banks.Count() == 0 && vwbanks.Count() == 0)
            {
                ViewBag.Message = "No Banks Information!";
                return View();
            }
            else if (banks.Count() == 0 && vwbanks.Count() > 0)
            {
                return View(vwbanks);
            }
            else
            {
                return View(banks);
            }          
        }

        // GET: Banks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bankinfo = repo.Find(id.Value);

            if (bankinfo == null)
            {
                return HttpNotFound();
            }
            return View(bankinfo);
        }

        // GET: Banks/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(clientRepo.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: Banks/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼,isDeleted")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶銀行資訊);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(clientRepo.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: Banks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bankinfo = repo.Find(id.Value);

            if (bankinfo == null)
            {
                return HttpNotFound();
            }

            ViewBag.客戶Id = new SelectList(clientRepo.All(), "Id", "客戶名稱", bankinfo.客戶Id);
            return View(bankinfo);
        }

        // POST: Banks/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            var bank = repo.Find(id);
            if (TryUpdateModel(bank))
            {
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(clientRepo.All(), "Id", "客戶名稱", bank.客戶Id);
            return View(bank);
        }

        // GET: Banks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bank = repo.Find(id.Value);
            if (bank == null)
            {
                return HttpNotFound();
            }
            return View(bank);
        }

        // POST: Banks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var bank = repo.Find(id);
            repo.Delete(bank);
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
