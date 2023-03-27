using QuanLyGPLX_LapTrinhWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyGPLX_LapTrinhWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            MyDataDataContext data = new MyDataDataContext();
            var Hang = data.HangGPLXes.Select(p => p.MaHang).ToList();
            ViewBag.HangGPLX = new SelectList(Hang, "TenHang");
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                Session["username"] = username;
                return RedirectToAction("DanhSachHoSo", "Admin/HoSo");
            }
            else
            {
                TempData["error"] = "Sai tên đăng nhập hoặc mật khẩu";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["username"] = null;
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult TraCuuHoSo(string MaGPLX, string HangGPLX)
        {
            using (MyDataDataContext data = new MyDataDataContext())
            {
                var HoSo = (from hs in data.HoSoGPLXes
                            join ll in data.LyLiches on hs.SoCCCD equals ll.SoCCCD
                            join px in data.PhuongXas on ll.DiaChi equals px.MaPX
                            join qh in data.QuanHuyens on px.MaHuyen equals qh.MaHuyen
                            join t in data.TinhTPs on qh.MaTinh equals t.MaTinh
                            join dt in data.DanTocs on ll.MaDT equals dt.MaDT
                            join qt in data.QuocTiches on ll.MaQT equals qt.MaQT
                            join ttsh in data.TrungTamSatHaches on hs.MaTT equals ttsh.MaTT
                            where hs.MaGPLX == MaGPLX && hs.MaHang == HangGPLX
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
                return PartialView(HoSo);
            }
        }
    }
}