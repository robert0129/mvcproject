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

namespace mvcproject.Controllers
{
    public class CustomersController : BaseController
    {
        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();
        // GET: Customers
        
        public ActionResult Index(string search, int? classification, Customer? sortItem, string order)
        {
            if (classification == null)
            {
                classification = 0;
            }

            if (sortItem == null)
            {
                sortItem = 0;
            }

            if (String.IsNullOrEmpty(order))
            {
                order = "up";
            }

            var customers = repo.All(search, classification.Value);
            var options = repo.All().Select(c => c.客戶分類);
            ViewBag.classification = new SelectList(options);

            if (customers.Count() == 0)
            {
                ViewBag.Message = "查無此客戶資料";
            }



            customers = customers.Sort(sortItem.Value, order);
            return View(customers);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = repo.Find(id.Value);

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,isDeleted")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶資料);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = repo.Find(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            var customer = repo.Find(id);

            if (TryUpdateModel(customer))
            {
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = repo.Find(id.Value);

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var customer = repo.Find(id);
            repo.Delete(customer);
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

        public FileResult ExportCustomers()
        {
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Customers");
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            var customers = repo.All().ToList();
            row1.CreateCell(0).SetCellValue("客戶名稱");
            row1.CreateCell(1).SetCellValue("統一編號");
            row1.CreateCell(2).SetCellValue("電話");
            row1.CreateCell(3).SetCellValue("傳真");
            row1.CreateCell(4).SetCellValue("地址");
            row1.CreateCell(5).SetCellValue("電子郵件");

            for (int i = 0; i < customers.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(customers[i].客戶名稱.ToString());
                rowtemp.CreateCell(1).SetCellValue(customers[i].統一編號.ToString());
                rowtemp.CreateCell(2).SetCellValue(customers[i].電話.ToString());
                rowtemp.CreateCell(3).SetCellValue(customers[i].傳真.ToString());
                rowtemp.CreateCell(4).SetCellValue(customers[i].地址.ToString());
                rowtemp.CreateCell(5).SetCellValue(customers[i].Email.ToString());
            }
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "Customers.xls");
        }
    }
}
