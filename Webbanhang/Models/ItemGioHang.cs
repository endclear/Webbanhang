using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webbanhang.Models
{
    public class ItemGioHang
    {
        public string tensp { get; set; }
        public int masp { get; set; }
        public int soluong { get; set; }
        public decimal dongia { get; set; }
        public decimal thanhtien { get; set; }
        public string hinhanh { get; set; }

        public ItemGioHang(int Masp)
        {
            using (QLbanhangEntities db = new QLbanhangEntities())
            {
                this.masp = Masp;
                sanpham sp = db.sanphams.Single(n => n.MaSP == Masp);
                this.tensp = sp.TenSP;
                this.hinhanh = sp.HinhAnh;
                this.dongia = sp.DonGia.Value;
                this.soluong = 1;
                this.thanhtien = dongia * soluong;
            }
        }
        public ItemGioHang(int Masp, int sl) { 
            using(QLbanhangEntities db = new QLbanhangEntities())
            {
                this.masp = Masp;
                sanpham sp = db.sanphams.Single(n => n.MaSP == Masp);
                this.tensp = sp.TenSP;
                this.hinhanh = sp.HinhAnh;
                this.dongia = sp.DonGia.Value;
                this.soluong = sl;
                this.thanhtien = dongia * soluong;
            }
        }
        public ItemGioHang() { }
    }
}