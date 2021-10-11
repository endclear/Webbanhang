using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Models;

namespace Webbanhang.Controllers
{
    [Authorize(Roles = "QuanTri")]
    public class NhapHangController : Controller
    {
        QLbanhangEntities db = new QLbanhangEntities();
        // GET: NhapHang
        public ActionResult Index()
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.MaNCC), "MaNCC", "TenNCC");
            ViewBag.Listsanpham = db.sanphams;
            return View();
        }
        [HttpPost]
        public ActionResult Index(PhieuNhap _phieunhap,IEnumerable<CT_phieunhap> lstChitietphieunhap)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.MaNCC), "MaNCC", "TenNCC");
            ViewBag.Listsanpham = db.sanphams;
            _phieunhap.DaXoa = false;
            db.PhieuNhaps.Add(_phieunhap);
            db.SaveChanges();
            foreach (var item in lstChitietphieunhap)
            {
                sanpham sp = db.sanphams.Single(n => n.MaSP == item.MaSP);
                sp.SoLuongTon += item.SoLuongNhap;
                item.MaPN = _phieunhap.MaPN;
            }
            db.CT_phieunhap.AddRange(lstChitietphieunhap);
            db.SaveChanges();
            return View();
        }

        public ActionResult SanPhamSapHetHang()
        {
            return View(db.sanphams.Where(n => n.SoLuongTon <= 5 && n.DaXoa == false));
        }

        public ActionResult BoSungSanPham(int id)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.MaNCC), "MaNCC", "TenNCC");
            try {
                var sp = db.sanphams.Single(n => n.MaSP == id && n.DaXoa == false && n.SoLuongTon <= 5);
                return View(sp);
            }
            catch(Exception ex)
            {
                return HttpNotFound(ex.ToString());
            }  
        }


        [HttpPost]
        public ActionResult BoSungSanPham(PhieuNhap _phieunhap,CT_phieunhap _ctphieunhap)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.MaNCC), "MaNCC", "TenNCC");
            _phieunhap.DaXoa = false;
            db.PhieuNhaps.Add(_phieunhap);
            db.SaveChanges();
            _ctphieunhap.MaPN = _phieunhap.MaPN;
            sanpham sp = db.sanphams.SingleOrDefault(n => n.MaSP == _ctphieunhap.MaSP);
            sp.SoLuongTon += _ctphieunhap.SoLuongNhap;
            db.CT_phieunhap.Add(_ctphieunhap);
            db.SaveChanges();
            return RedirectToAction("SanPhamSapHetHang");
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