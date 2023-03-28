using PagedList;
using QuanLyGPLX_LapTrinhWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyGPLX_LapTrinhWeb.Areas.Admin.Controllers
{
    public class TrungTamSatHachController : Controller
    {
        // GET: Admin/TrungTamSatHach
        MyDataDataContext data = new MyDataDataContext();

        #region danh sách trung tâm sát hạch
        public ActionResult DanhSachTrungTam(int? page, string search)
        {
            if (page == null)
                page = 1;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            if (search == null)
            {
                var dstt = data.TrungTamSatHaches.ToList();
                return View(dstt.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                var dstt = data.TrungTamSatHaches.Where(t => t.MaTT.Contains(search) || t.TenTT.Contains(search)).ToList();
                if (dstt.Count == 0)
                    ViewBag.ThongBao = "Không tìm thấy trung tâm sát hạch nào";
                return View(dstt.ToPagedList(pageNumber, pageSize));
            }
        }
        #endregion

        #region chỉnh sửa trung tâm sát hạch
        public ActionResult Edit(string id)
        {
            var tt = data.TrungTamSatHaches.First(t => t.MaTT == id);
            return View(tt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var E_TTSH = data.TrungTamSatHaches.SingleOrDefault(m => m.MaTT == id);
            var E_TenTT = collection["TenTT"];
            if (E_TTSH == null)
            {
                ViewData["Error"] = "Don't empty";
            }
            else
            {
                E_TTSH.TenTT = E_TenTT;
                TryUpdateModel(E_TTSH);
                data.SubmitChanges();
                return RedirectToAction("DanhSachTrungTam");
            }
            return this.Edit(id);
        }
        #endregion

        #region xóa trung tâm sát hạch
        public ActionResult DeleteTrungTam(string id)
        {
            var MaTT = data.TrungTamSatHaches.Where(a => a.MaTT == id).FirstOrDefault();
            data.TrungTamSatHaches.DeleteOnSubmit(MaTT);
            data.SubmitChanges();
            return Json(new { status = "Success" });
        }
        #endregion

        #region thêm trung tâm sát hạch
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            var C_MaTT = collection["MaTT"];
            var C_TenTT = collection["TenTT"];
            if (C_MaTT == null || C_TenTT == null)
            {
                ViewData["Error"] = "Don't empty";
            }
            else
            {
                TrungTamSatHach ttsh = new TrungTamSatHach();
                ttsh.MaTT = C_MaTT;
                ttsh.TenTT = C_TenTT;
                data.TrungTamSatHaches.InsertOnSubmit(ttsh);
                data.SubmitChanges();
                return RedirectToAction("DanhSachTrungTam");
            }
            return this.Create();
        }
        #endregion
    }
}