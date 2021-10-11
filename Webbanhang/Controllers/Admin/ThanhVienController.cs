using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Models;

namespace Webbanhang.Controllers.Admin
{
    [Authorize(Roles = "QuanTri")]
    public class ThanhVienController : Controller
    {
        QLbanhangEntities db = new QLbanhangEntities();
        // GET: ThanhVien
        public ActionResult Index()
        {
            try {
                var thanhvien = db.thanhviens.OrderBy(n => n.HoTen);
                return View(thanhvien);
            }
            catch(Exception ex)
            {
                return HttpNotFound(ex.ToString());
            }
        }

        public ActionResult PhanQuyen(int id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            thanhvien tv = db.thanhviens.SingleOrDefault(n => n.MaTV == id);
            if (tv == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role = db.Roles;
            ViewBag.Thanhvien_Role = db.ThanhVien_Role.Where(n => n.MaTV == id);
            return View(tv);
        }

        [HttpPost]
        public ActionResult PhanQuyen(int id,IEnumerable<ThanhVien_Role> list_thanhvien_role)
        {
            //neu thanh vien da phan quyen roi va muon phan quyen lai
            //lay danh sach quyen da phan va xoa 
            var lstDaPhanQuyen = db.ThanhVien_Role.Where(n => n.MaTV == id);
            if (lstDaPhanQuyen != null)
            {
                db.ThanhVien_Role.RemoveRange(lstDaPhanQuyen);
                db.SaveChanges();
            }
            //cap nhat quyen moi da them cho thanh vien 
            if (list_thanhvien_role != null)
            {
                foreach (var item in list_thanhvien_role)
                {
                    item.MaTV = id;
                    db.ThanhVien_Role.Add(item);
                }
                db.SaveChanges();
            }
            
            return RedirectToAction("Index");
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