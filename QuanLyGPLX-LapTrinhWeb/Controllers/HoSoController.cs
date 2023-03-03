using QuanLyGPLX_LapTrinhWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyGPLX_LapTrinhWeb.Controllers
{
    public class HoSoController : Controller
    {
        // GET: HoSo
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult DanhSachHoSo()
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
            return View(all_HoSo);
        }

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
                          where hs.MaGPLX.Contains(id)
                          select new HoSo
                          {
                              HinhAnh = ll.HinhAnh,
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
    }
}