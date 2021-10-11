 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Models;
using PagedList;

namespace Webbanhang.Controllers
{
    public class TimkiemController : Controller
    {
        // GET: Timkiem
        QLbanhangEntities db = new QLbanhangEntities();
        [HttpGet]
        public ActionResult KQTimkiem(string sTukhoa,int? page)
        {
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            //thuwjc hien phan trang
            //tao bien so sp tren trang
            int pagesize = 6;
            //số trang hiện tại
            int pagenumber = (page ?? 1);
            //tim kiem theo ten sp
            var lstSP = db.sanphams.Where(n => n.TenSP.Contains(sTukhoa));
            ViewBag.Tukhoa = sTukhoa;
            return View(lstSP.OrderBy(n=>n.TenSP).ToPagedList(pagenumber,pagesize));
        }
        [HttpPost]
        public ActionResult layKQTimkiem(string sTukhoa)
        {
            return RedirectToAction("KQTimkiem", new { @sTukhoa = sTukhoa }) ;
        }
        public ActionResult timkiemPartial(string sTukhoa)
        {
            //tim kiem theo ten sp
            var lstSP = db.sanphams.Where(n => n.TenSP.Contains(sTukhoa));
            ViewBag.Tukhoa = sTukhoa;
            return PartialView(lstSP.OrderBy(n=>n.DonGia));
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