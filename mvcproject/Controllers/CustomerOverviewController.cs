using mvcproject.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace mvcproject.Controllers
{
    public class CustomerOverviewController : BaseController
    {
        vw_customerlistsRepository repo = RepositoryHelper.Getvw_customerlistsRepository();
        // GET: CustomerOverview
        public ActionResult Index(int? page, string search)
        {
            int pageNums = page ?? 1;
            int pageSize = 5;
            var v = repo.All().OrderBy(o => o.客戶名稱).ToList();
            var vw = v.ToPagedList(pageNums, pageSize);
            ViewBag.OnePageOfVWs = vw;

            if (String.IsNullOrEmpty(search))
            {
                return View(vw);
            }

            var svw = repo.All().Where(c => c.客戶名稱.Contains(search));
            if (svw.Count() == 0)
            {
                ViewBag.Message = "無此客戶資訊 - " + search;
                return View(svw.ToPagedList(pageNums, pageSize));
            }

            vw = svw.ToPagedList(pageNums, pageSize);

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

        public FileResult ExportOverview()
        {
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Overview");
            //获取list数据
            var overview = repo.All().ToList();
            //List<TB_STUDENTINFOModel> listRainInfo = m_BLL.GetSchoolListAATQ(schoolname);
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            
            row1.CreateCell(0).SetCellValue("客戶名稱");
            row1.CreateCell(1).SetCellValue("聯絡人數量");
            row1.CreateCell(2).SetCellValue("銀行帳戶數量");
            //将数据逐步写入sheet1各个行
            for (int i = 0; i < overview.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(overview[i].客戶名稱.ToString());
                rowtemp.CreateCell(1).SetCellValue(overview[i].聯絡人數量.ToString());
                rowtemp.CreateCell(2).SetCellValue(overview[i].銀行帳戶數量.ToString());
            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "CustomersOverView.xls");
        }

        public ActionResult BoostrapTemplate()
        {
            return View();
        }
    }
}