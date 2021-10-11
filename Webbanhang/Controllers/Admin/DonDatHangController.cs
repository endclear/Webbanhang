using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Models;

namespace Webbanhang.Controllers
{
    [Authorize(Roles = "QuanTri")]
    public class DonDatHangController : Controller
    {
        QLbanhangEntities db = new QLbanhangEntities();
        // GET: DuyetDonHang
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var ddh = db.dondathangs.OrderBy(n => n.NgayDat);
                return View(ddh);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.ToString());
            }
            
        }
        [HttpPost]
        public ActionResult Index(int key)
        {
            if(key == 1) {
                try
                {
                    var ddh = db.dondathangs.Where(n => n.DaThanhToan == false && n.Daxoa == false && n.DaHuy == false).OrderBy(n => n.NgayDat);
                    return View(ddh);
                }
                catch (Exception ex)
                {
                    return HttpNotFound(ex.ToString());
                }
            }
            if (key == 2)
            {
                try
                {
                    var ddh = db.dondathangs.Where(n => n.TinhTrangGiao ==false &&n.DaThanhToan==true && n.Daxoa == false && n.DaHuy == false).OrderBy(n => n.NgayDat);
                    return View(ddh);
                }
                catch (Exception ex)
                {
                    return HttpNotFound(ex.ToString());
                }
            }
            if (key == 3)
            {
                try
                {
                    var ddh = db.dondathangs.Where(n => n.DaThanhToan == true && n.TinhTrangGiao == true && n.Daxoa == false && n.DaHuy == false).OrderBy(n => n.NgayDat);
                    return View(ddh);
                }
                catch (Exception ex)
                {
                    return HttpNotFound(ex.ToString());
                }
            }
            try
            {
                var ddh = db.dondathangs.OrderBy(n => n.NgayDat);
                return View(ddh);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.ToString());
            }
        }

        public ActionResult DuyetDonHang(int? id)
        {
            try {
                ViewBag.listChiTietDDH = db.CT_DonDH.Where(n => n.MaDonDH == id);
                var ddh = db.dondathangs.Single(n => n.MadonDH == id);
                return View(ddh);
            }
            catch(Exception ex)
            {
                return HttpNotFound(ex.ToString());
            }
        }
        [HttpPost]
        public ActionResult DuyetDonHang(int? MadonDH,FormCollection f)
        {
            dondathang ddh = db.dondathangs.SingleOrDefault(n => n.MadonDH == MadonDH);
            ddh.TinhTrangGiao = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //try {
        //        var ddh = db.dondathangs.Where(n => n.DaThanhToan == true && n.TinhTrangGiao == true && n.Daxoa == false && n.DaHuy == false).OrderBy(n => n.NgayDat);
        //        return View(ddh);
        //    }
        //            catch(Exception ex)
        //            {
        //                return HttpNotFound(ex.ToString());
        //}
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