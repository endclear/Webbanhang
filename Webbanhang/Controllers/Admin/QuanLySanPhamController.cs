using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Models;

namespace Webbanhang.Controllers
{
    [Authorize(Roles = "QuanTri")]
    public class QuanLySanPhamController : Controller
    {

        QLbanhangEntities db = new QLbanhangEntities();
        // GET: QuanLySanPham
        public ActionResult Index()
        {
            return View(db.sanphams.Where(n => n.DaXoa == false).OrderByDescending(n => n.MaSP));
        }
        [HttpGet]
        public ActionResult Create() {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.MaNCC), "MaNCC", "TenNCC");
            ViewBag.MaNSX = new SelectList(db.NhaSXes.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX");
            ViewBag.LoaiSP = new SelectList(db.loaisanphams.OrderBy(n => n.MaLoai), "MaLoai", "TenLoai");
            return View();
        }
        [HttpPost]
        public ActionResult Create(sanpham _sanpham, HttpPostedFileBase HinhAnh)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.MaNCC), "MaNCC", "TenNCC");
            ViewBag.MaNSX = new SelectList(db.NhaSXes.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX");
            ViewBag.LoaiSP = new SelectList(db.loaisanphams.OrderBy(n => n.MaLoai), "MaLoai", "TenLoai");
            _sanpham.HinhAnh = checkImage(HinhAnh); 
            _sanpham.DaXoa = false; 
            db.sanphams.Add(_sanpham); 
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public String checkImage(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var filename = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/sanpham"), filename);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Upload = "Hình đã tồn tại";
                }
                else
                {
                    file.SaveAs(path);
                    Session["tenhinh"] = file.FileName;
                    ViewBag.Tenhinh = "";
                }
                return filename;
            }
            return null;
        }

        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            sanpham sp = db.sanphams.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.MaNCC), "MaNCC", "TenNCC",sp.MaNCC);
            ViewBag.MaNSX = new SelectList(db.NhaSXes.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX",sp.MaNSX);
            ViewBag.LoaiSP = new SelectList(db.loaisanphams.OrderBy(n => n.MaLoai), "MaLoai", "TenLoai",sp.MaLoai);
            return View(sp);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(sanpham _sanpham)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.MaNCC), "MaNCC", "TenNCC",_sanpham.NhaCungCap);
            ViewBag.MaNSX = new SelectList(db.NhaSXes.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX",_sanpham.MaNSX);
            ViewBag.LoaiSP = new SelectList(db.loaisanphams.OrderBy(n => n.MaLoai), "MaLoai", "TenLoai",_sanpham.MaLoai);
            _sanpham.DaXoa = false;
            db.Entry(_sanpham).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return View();
        }

        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            sanpham sp = db.sanphams.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            sp.DaXoa = true;
            db.Entry(sp).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index","QuanLySanPham");
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