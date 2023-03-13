using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyGPLX_LapTrinhWeb.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
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
                return RedirectToAction("DanhSachHoSo", "HoSo");
            }
            else
            {
                TempData["error"] = "Sai tên đăng nhập hoặc mật khẩu";
                return View();
            }
        }

    }
}