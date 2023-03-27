using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyGPLX_LapTrinhWeb.App_Start
{
    public class AdminAuthorize : AuthorizeAttribute
    {
        public int idChucNang { get; set; }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["username"] == null)
            {
                filterContext.Result = new RedirectResult("~/Home/Login");
            }
            else
            {
                if (idChucNang == 1)
                {
                    if (filterContext.HttpContext.Session["role"].ToString() != "1")
                    {
                        filterContext.Result = new RedirectResult("~/Admin/HoSo/AccessDenied");
                    }
                }
            }
        }
    }
}