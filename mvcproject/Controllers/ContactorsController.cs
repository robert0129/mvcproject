using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvcproject.Models;
using static mvcproject.Models.RepositoryHelper;
using System.IO;
using System.Data.Entity.Validation;

namespace mvcproject.Controllers
{
    [HandleError(ExceptionType = typeof(DbEntityValidationException), View = "Error_DbEntityValidationException")]
    public class ContactorsController : BaseController
    {
        客戶聯絡人Repository repo = RepositoryHelper.Get客戶聯絡人Repository();
        客戶資料Repository clientRepo = RepositoryHelper.Get客戶資料Repository();

        // GET: Contactors
        public ActionResult Index(string search, string JobTitle, Contactor? sortItem, string order)
        {
            if (sortItem == null)
            {
                sortItem = 0;
            }

            var jtitle = repo.All().Select(c => c.職稱);
            ViewBag.JobTitle = new SelectList(jtitle);
            
            var contactors = repo.All(search, JobTitle);

            if (contactors.Count() == 0)
            {
                ViewBag.Message = "查無此聯絡人資料";
            }

            contactors = contactors.Sort(sortItem.Value, order);
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

            ViewBag.客戶Id = new SelectList(clientRepo.All(), "Id", "客戶名稱");
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

        public FileResult ExportContacotrs()
        {
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Contactors");
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            var contactors = repo.All().ToList();
            row1.CreateCell(0).SetCellValue("職稱");
            row1.CreateCell(1).SetCellValue("姓名");
            row1.CreateCell(2).SetCellValue("電子郵件");
            row1.CreateCell(3).SetCellValue("手機");
            row1.CreateCell(4).SetCellValue("電話");
            row1.CreateCell(5).SetCellValue("客戶名稱");

            for (int i = 0; i < contactors.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(contactors[i].職稱.ToString());
                rowtemp.CreateCell(1).SetCellValue(contactors[i].姓名.ToString());
                rowtemp.CreateCell(2).SetCellValue(contactors[i].Email.ToString());
                rowtemp.CreateCell(3).SetCellValue(contactors[i].手機.ToString());
                rowtemp.CreateCell(4).SetCellValue(contactors[i].電話.ToString());
                rowtemp.CreateCell(5).SetCellValue(contactors[i].客戶資料.客戶名稱.ToString());
            }
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "Contactors.xls");
        }

        public ActionResult ContactorsList()
        {
            var c = repo.All().ToList().Take(5);
            return View(c);
        }

        public ActionResult BatchUpdate(ContactorModel[] items)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    客戶聯絡人 c = db.客戶聯絡人.Find(item.Id);
                    c.職稱 = item.職稱;
                    c.手機 = item.手機;
                    c.電話 = item.電話;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
