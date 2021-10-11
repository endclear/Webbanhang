using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Models;

namespace Webbanhang.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        QLbanhangEntities db = new QLbanhangEntities();
        public ActionResult Index()
        {
            return Redirect("QuanLySanPham/Index");
        }

        public JsonResult ThongKeAjax()
        {
            string UserVisit = HttpContext.Application["UserVisit"].ToString();
            string UserOnline = HttpContext.Application["UserOnline"].ToString();

            return Json(new { visit = UserVisit, online = UserOnline }); ;
         }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}