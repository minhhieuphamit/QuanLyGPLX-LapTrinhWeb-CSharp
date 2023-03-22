using PagedList;
using QuanLyGPLX_LapTrinhWeb.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace QuanLyGPLX_LapTrinhWeb.Areas.Admin.Controllers
{
    public class HoSoController : Controller
    {
        // GET: Admin/HoSo
        MyDataDataContext data = new MyDataDataContext();

        /*---------Danh sách hồ sơ---------*/
        public ActionResult DanhSachHoSo(int? page, string search)
        {
            if (page == null)
                page = 1;
            int pageSize = 5;
            int pageNum = page ?? 1;
            if (search == null)
            {
                var all_HoSo = (from hs in data.HoSoGPLXes
                                join ll in data.LyLiches on hs.SoCCCD equals ll.SoCCCD
                                join px in data.PhuongXas on ll.DiaChi equals px.MaPX
                                join qh in data.QuanHuyens on px.MaHuyen equals qh.MaHuyen
                                join t in data.TinhTPs on qh.MaTinh equals t.MaTinh
                                orderby ll.Ten, ll.HoLot ascending
                                select new HoSo
                                {
                                    MaGPLX = hs.MaGPLX,
                                    HoTen = ll.HoLot + " " + ll.Ten,
                                    NgaySinh = String.Format("{0:dd/MM/yyyy}", ll.NgaySinh),
                                    GioiTinh = ll.GioiTinh,
                                    DiaChi = px.TenPX + ", " + qh.TenHuyen + ", " + t.TenTinh,
                                    NgayCap = String.Format("{0:dd/MM/yyyy}", hs.NgayCapGPLX),
                                    NgayHetHan = String.Format("{0:dd/MM/yyyy}", hs.NgayHetHanGPLX),
                                    HangGPLX = hs.MaHang
                                });
                return View(all_HoSo.ToPagedList(pageNum, pageSize));
            }
            else
            {
                var all_HoSo = (from hs in data.HoSoGPLXes
                                join ll in data.LyLiches on hs.SoCCCD equals ll.SoCCCD
                                join px in data.PhuongXas on ll.DiaChi equals px.MaPX
                                join qh in data.QuanHuyens on px.MaHuyen equals qh.MaHuyen
                                join t in data.TinhTPs on qh.MaTinh equals t.MaTinh
                                orderby ll.Ten, ll.HoLot ascending
                                where hs.MaGPLX.Contains(search)
                                select new HoSo
                                {
                                    MaGPLX = hs.MaGPLX,
                                    HoTen = ll.HoLot + " " + ll.Ten,
                                    NgaySinh = String.Format("{0:dd/MM/yyyy}", ll.NgaySinh),
                                    GioiTinh = ll.GioiTinh,
                                    DiaChi = px.TenPX + ", " + qh.TenHuyen + ", " + t.TenTinh,
                                    NgayCap = String.Format("{0:dd/MM/yyyy}", hs.NgayCapGPLX),
                                    NgayHetHan = String.Format("{0:dd/MM/yyyy}", hs.NgayHetHanGPLX),
                                    HangGPLX = hs.MaHang
                                });
                return View(all_HoSo.ToPagedList(pageNum, pageSize));
            }

        }

        /*---------Chi tiết hồ sơ---------*/
        public ActionResult Details(string id)
        {
            var D_HoSo = (from hs in data.HoSoGPLXes
                          join ll in data.LyLiches on hs.SoCCCD equals ll.SoCCCD
                          join px in data.PhuongXas on ll.DiaChi equals px.MaPX
                          join qh in data.QuanHuyens on px.MaHuyen equals qh.MaHuyen
                          join t in data.TinhTPs on qh.MaTinh equals t.MaTinh
                          join dt in data.DanTocs on ll.MaDT equals dt.MaDT
                          join qt in data.QuocTiches on ll.MaQT equals qt.MaQT
                          join ttsh in data.TrungTamSatHaches on hs.MaTT equals ttsh.MaTT
                          where hs.MaGPLX == id
                          select new HoSo
                          {
                              HinhAnh = hs.HinhAnh,
                              MaGPLX = hs.MaGPLX,
                              HoTen = ll.HoLot + " " + ll.Ten,
                              NgaySinh = String.Format("{0:dd/MM/yyyy}", ll.NgaySinh),
                              GioiTinh = ll.GioiTinh,
                              SDT = ll.SDT,
                              DanToc = dt.TenDT,
                              DiaChi = px.TenPX + ", " + qh.TenHuyen + ", " + t.TenTinh,
                              QuocTich = qt.TenQT,
                              NgayCap = String.Format("{0:dd/MM/yyyy}", hs.NgayCapGPLX),
                              NgayHetHan = String.Format("{0:dd/MM/yyyy}", hs.NgayHetHanGPLX),
                              HangGPLX = hs.MaHang,
                              DiemLT = hs.DiemLT.ToString(),
                              DiemTH = hs.DiemTH.ToString(),
                              TTSH = ttsh.TenTT
                          }).First();
            return View(D_HoSo);
        }

        /*---------Chỉnh sửa hồ sơ---------*/
        public ActionResult Edit(string id)
        {
            var Hang = data.HangGPLXes.Select(p => p.MaHang).ToList();
            ViewBag.HangGPLX = new SelectList(Hang, "TenHang");

            var TenTTSH = data.TrungTamSatHaches.Select(p => new { p.MaTT, p.TenTT }).ToList();
            ViewBag.TenTTSH = new SelectList(TenTTSH, "MaTT", "TenTT", data.HoSoGPLXes.Where(p => p.MaGPLX == id).Select(p => p.MaTT).First());

            var E_MaGPLX = data.HoSoGPLXes.First(m => m.MaGPLX == id);
            var E_NgayCap = data.HoSoGPLXes.First(m => m.MaGPLX == id);
            ViewBag.NgayCap = String.Format("{0:dd/MM/yyyy}", E_NgayCap.NgayCapGPLX);

            var E_NgayHetHan = data.HoSoGPLXes.First(m => m.MaGPLX == id);
            ViewBag.NgayHetHan = String.Format("{0:dd/MM/yyyy}", E_NgayHetHan.NgayHetHanGPLX);
            return View(E_MaGPLX);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var E_GPLX = data.HoSoGPLXes.SingleOrDefault(m => m.MaGPLX == id);
            var E_MaHang = collection["MaHang"];
            var E_HinhAnh = collection["HinhAnh"];
            var E_NgayCap = collection["NgayCapGPLX"];
            var E_NgayHetHan = collection["NgayHetHanGPLX"];
            var E_DiemLT = collection["DiemLT"];
            var E_DiemTH = collection["DiemTH"];
            var E_MaTTSH = collection["TenTT"];
            if (E_GPLX == null)
            {
                ViewData["Error"] = "Don't empty";
            }
            else
            {
                E_GPLX.MaHang = E_MaHang;
                E_GPLX.HinhAnh = E_HinhAnh;
                E_GPLX.NgayCapGPLX = DateTime.ParseExact(E_NgayCap, "MM/dd/yyyy", null);
                E_GPLX.NgayHetHanGPLX = DateTime.ParseExact(E_NgayHetHan, "MM/dd/yyyy", null);
                E_GPLX.DiemLT = int.Parse(E_DiemLT);
                E_GPLX.DiemTH = int.Parse(E_DiemTH);
                E_GPLX.MaTT = E_MaTTSH;
                TryUpdateModel(E_GPLX);
                data.SubmitChanges();
                return RedirectToAction("DanhSachHoSo");
            }
            return this.Edit(id);
        }

        /*---------Xóa hồ sơ---------*/
        public ActionResult Delete(string id)
        {
            var D_MaGPLX = data.HoSoGPLXes.First(m => m.MaGPLX == id);
            return View(D_MaGPLX);
        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var D_MaGPLX = data.HoSoGPLXes.First(m => m.MaGPLX == id);
            data.HoSoGPLXes.DeleteOnSubmit(D_MaGPLX);
            data.SubmitChanges();
            return RedirectToAction("DanhSachHoSo");
        }

        /*---------Thêm mới hồ sơ---------*/
        [HttpPost]
        public JsonResult GetSoCCCD(string Prefix)
        {
            var SoCCCD = (from CCCD in data.LyLiches
                          where CCCD.SoCCCD.StartsWith(Prefix)
                          select new
                          {
                              CCCD.SoCCCD
                          }).ToList();
            return Json(SoCCCD, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            var Hang = data.HangGPLXes.Select(p => p.MaHang).ToList();
            ViewBag.HangGPLX = new SelectList(Hang, "TenHang");

            var TenTTSH = data.TrungTamSatHaches.Select(p => p.TenTT).ToList();
            ViewBag.TenTTSH = new SelectList(TenTTSH, "TenTT");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            var C_HinhAnh = collection["HinhAnh"];
            var C_SoCCCD = collection["SoCCCD"];
            var C_MaGPLX = collection["MaGPLX"];
            var C_MaHang = collection["MaHang"];
            var C_NgayCapGPLX = collection["NgayCapGPLX"];
            var C_NgayHetHanGPLX = collection["NgayHetHanGPLX"];
            var C_DiemLT = collection["DiemLT"];
            var C_DiemTH = collection["DiemTH"];
            var C_TenTT = collection["TenTT"];
            if (C_MaGPLX == "" || C_MaHang == null || C_NgayCapGPLX == null || C_NgayHetHanGPLX == null || C_DiemLT == null || C_DiemTH == null || C_TenTT == null)
            {
                ViewData["Error"] = "Don't empty";
            }

            else
            {
                var C_MaTT = data.TrungTamSatHaches.First(m => m.TenTT == C_TenTT).MaTT;
                HoSoGPLX hs = new HoSoGPLX();
                hs.HinhAnh = C_HinhAnh;
                hs.SoCCCD = C_SoCCCD;
                hs.MaGPLX = C_MaGPLX;
                hs.MaHang = C_MaHang;
                hs.NgayCapGPLX = DateTime.Parse(C_NgayCapGPLX);
                hs.NgayHetHanGPLX = DateTime.Parse(C_NgayHetHanGPLX);
                hs.DiemLT = int.Parse(C_DiemLT);
                hs.DiemTH = int.Parse(C_DiemTH);
                hs.MaTT = C_MaTT;
                data.HoSoGPLXes.InsertOnSubmit(hs);
                data.SubmitChanges();
                return RedirectToAction("DanhSachHoSo");
            }
            return this.Create();
        }

        /*---------Upload hình ảnh hồ sơ---------*/
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
    }
}