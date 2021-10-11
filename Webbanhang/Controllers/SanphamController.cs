using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Models;
using PagedList;

namespace Webbanhang.Controllers
{
    public class SanphamController : Controller
    {
        // GET: Sanpham
        QLbanhangEntities db = new QLbanhangEntities();
        [ChildActionOnly]
        public ActionResult SanphamStyle1Partial()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult SanphamStyle2Partial()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult SanphamPartial()
        {
            var lstSanphamLTM = db.sanphams.Where(n => n.MaLoai == 2 && n.Moi == 1);
            return PartialView(lstSanphamLTM);
        }
        //trang chi tiết
        public ActionResult XemChitiet(int? id,string bidanh)
        {
            //kiểm tra số truyền vào có rỗng k
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //nếu k thì lấy ra sp tương ứng
            sanpham sp = db.sanphams.SingleOrDefault(n => n.MaSP == id && n.DaXoa==false&&n.Bidanh==bidanh);
            if (sp == null)
            {
                return HttpNotFound();//thông báo nếu k tìm đc
            }
            return View(sp);
        } 
        //xây dựng 1 action load sản phẩm theo maloaisp và mã nhà sx
        public ActionResult loadSanpham(int? maloaisp,int? maNSX,int? page)
        {
            if (maloaisp == null || maNSX==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lstSP = db.sanphams.Where(n => n.MaLoai == maloaisp && n.MaNSX == maNSX);
            if (lstSP.Count()==0) {
                return HttpNotFound();
            }
            //thuwjc hien phan trang
            //tao bien so sp tren trang
            int pagesize = 6;
            //số trang hiện tại
            int pagenumber = (page ?? 1);
            ViewBag.maloaisp = maloaisp;
            ViewBag.maNSX = maNSX;
            return View(lstSP.OrderBy(n=>n.MaSP).ToPagedList(pagenumber,pagesize));
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