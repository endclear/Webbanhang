using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Models;

namespace Webbanhang.Controllers
{
    public class GioHangController : Controller
    {
        QLbanhangEntities db = new QLbanhangEntities();
        //lấy giỏ hàng
        public List<ItemGioHang> Laygiohang()
        {
            //gior hang da ton tai
            List<ItemGioHang> lstGiohang = Session["giohang"] as List<ItemGioHang>;
            if (lstGiohang == null)
            {
                //neu chua ton tai session gio hang thi tao list moi
                lstGiohang = new List<ItemGioHang>();
                Session["giohang"] = lstGiohang;
                return lstGiohang;
            }
            return lstGiohang;
        }

        //them gio hang(load trang)
        public ActionResult themgiohang(int masp,string sUrl)
        {
            //ktra sp ton tai trong DB hay ko
            sanpham sp = db.sanphams.SingleOrDefault(n => n.MaSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lay gio hang
            List<ItemGioHang> lstGiohang = Laygiohang();
            //neesu sp da ton tai trong gio hang
            ItemGioHang spcheck = lstGiohang.SingleOrDefault(n => n.masp == masp);
            if (spcheck != null)
            {
                if (sp.SoLuongTon < spcheck.soluong)
                {
                    return View("Thongbao");
                }
                spcheck.soluong++;
                spcheck.thanhtien = spcheck.soluong * spcheck.dongia;
                return Redirect(sUrl);
            }
            ItemGioHang itemGH = new ItemGioHang(masp);
            if (sp.SoLuongTon < itemGH.soluong)
            {
                return View("Thongbao");
            }
            
            lstGiohang.Add(itemGH);
            return Redirect(sUrl);
        }

        //tinh so luong
        public double tinhtongsoluong()
        {
            //lay gio hang
            List<ItemGioHang> lstGiohang = Session["giohang"] as List<ItemGioHang>;
            if(lstGiohang == null)
            {
                return 0;
            }
            return lstGiohang.Sum(n => n.soluong);
        }

        //tinh tong tien
        public decimal tinhtongtien()
        {
            List<ItemGioHang> lstGiohang = Session["giohang"] as List<ItemGioHang>;
            if (lstGiohang == null)
            {
                return 0;
            }
            return lstGiohang.Sum(n => n.thanhtien);
        }


        public ActionResult GiohangPartial()
        {
            if (tinhtongsoluong() == 0)
            {
                ViewBag.Tongsoluong = 0;
                ViewBag.Tongtien = 0;
                return PartialView();
            }
            else
            {
                ViewBag.Tongsoluong = tinhtongsoluong();
                ViewBag.Tongtien = tinhtongtien();
            }
            return PartialView();
        }
        // GET: GioHang
        public ActionResult Xemgiohang()
        {
            //laasy gio hang
            List<ItemGioHang> lstGiohang = Laygiohang();
            return View(lstGiohang);
        }
        //chỉnh suwea gio hang
        [HttpGet]
        public ActionResult SuaGiohang(int masp)
        {
            //ktra session gio hang co ton tai khong
            if (Session["giohang"] == null)
            {
                return RedirectToAction("Index", "Home"); 
            }
            //ktra sp ton tai trong DB hay ko
            sanpham sp = db.sanphams.SingleOrDefault(n => n.MaSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lay ra gio hang tu session
            List<ItemGioHang> lstGiohang = Laygiohang();
            //ktra xem san pham co ton tai trong gio hang ko
            ItemGioHang spCheck = lstGiohang.SingleOrDefault(n => n.masp == masp);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Giohang = lstGiohang;
            return View(spCheck);
        }
        //cap nhat gio hang
        [HttpPost]
        public ActionResult capnhatgiohang(ItemGioHang itemGH)
        {
            //ktra sl ton
            sanpham spCheck = db.sanphams.Single(n => n.MaSP == itemGH.masp);
            if (spCheck.SoLuongTon < itemGH.soluong)
            {
                return View("Thongbao");
            }
            //cap nhat so luong trong session gio hang
            //lay list gio hang tu session
            List<ItemGioHang> lstGH = Laygiohang();
            //lay sp can cap nhat trong list ra
            ItemGioHang itemGHUpdate = lstGH.Find(n => n.masp == itemGH.masp);
            //cap nhat so luong va tong tien
            itemGHUpdate.soluong = itemGH.soluong;
            itemGHUpdate.thanhtien = itemGHUpdate.soluong * itemGHUpdate.dongia;
            return RedirectToAction("Xemgiohang");
        }
        public ActionResult xoagiohang(int masp)
        {
            //ktra session gio hang co ton tai khong
            if (Session["giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //ktra sp ton tai trong DB hay ko
            sanpham sp = db.sanphams.SingleOrDefault(n => n.MaSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lay ra gio hang tu session
            List<ItemGioHang> lstGiohang = Laygiohang();
            //ktra xem san pham co ton tai trong gio hang ko
            ItemGioHang spCheck = lstGiohang.SingleOrDefault(n => n.masp == masp);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            lstGiohang.Remove(spCheck);
            return RedirectToAction("Xemgiohang");
        }
        public ActionResult Dathang()
        {
            //ktra session gio hang co ton tai khong
            if (Session["giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Khachhang kHang = new Khachhang();
            if (Session["Taikhoan"] != null)
            {
                thanhvien tv = Session["Taikhoan"] as thanhvien;
                kHang.TenKH = tv.HoTen;
                kHang.DiaChi = tv.DiaChi;
                kHang.Email = tv.Email;
                kHang.MaTV = tv.MaTV;
                kHang.DienThoai = tv.DienThoai;
                db.Khachhangs.Add(kHang);
                db.SaveChanges();
            }
            dondathang ddh = new dondathang();
            ddh.NgayDat = DateTime.Now;
            ddh.MaKH = kHang.MaKH;
            ddh.TinhTrangGiao = false;
            ddh.DaThanhToan = false;
            ddh.UuDai = 0;
            ddh.Daxoa = false;
            ddh.DaHuy = false;
            db.dondathangs.Add(ddh);
            db.SaveChanges();
            //them chi tiet don dat hang
            List<ItemGioHang> lstGH = Laygiohang();
            foreach (var item in lstGH)
            {
                CT_DonDH ctdh = new CT_DonDH();
                ctdh.MaDonDH = ddh.MadonDH;
                ctdh.MaSP = item.masp;
                ctdh.SoLuong = item.soluong;
                ctdh.DonGia = (int)item.dongia;
                db.CT_DonDH.Add(ctdh);
            }
            db.SaveChanges();
            Session["giohang"] = null;
            
            return RedirectToAction("Xemgiohang");
        }
        //them gio hang Ajax
        public ActionResult themgiohangAjax(int masp)
        {
            //ktra sp ton tai trong DB hay ko
            sanpham sp = db.sanphams.SingleOrDefault(n => n.MaSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lay gio hang
            List<ItemGioHang> lstGiohang = Laygiohang();
            //neesu sp da ton tai trong gio hang
            ItemGioHang spcheck = lstGiohang.SingleOrDefault(n => n.masp == masp);
            if (spcheck != null)
            {
                if (sp.SoLuongTon < spcheck.soluong)
                {
                    return null;
                }
                spcheck.soluong++;
                spcheck.thanhtien = spcheck.soluong * spcheck.dongia;
                ViewBag.Tongtien = tinhtongtien();
                ViewBag.Tongsoluong = tinhtongsoluong();
                return PartialView("GiohangPartial");
            }
            ItemGioHang itemGH = new ItemGioHang(masp);
            if (sp.SoLuongTon < itemGH.soluong)
            {
                return null;
            }

            lstGiohang.Add(itemGH);
            ViewBag.Tongtien = tinhtongtien();
            ViewBag.Tongsoluong = tinhtongsoluong();
            return PartialView("GiohangPartial");
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