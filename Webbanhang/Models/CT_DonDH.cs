//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Webbanhang.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CT_DonDH
    {
        public int MaDonDH { get; set; }
        public int MaSP { get; set; }
        public int SoLuong { get; set; }
        public int DonGia { get; set; }
        public int MaCtDonDH { get; set; }
    
        public virtual dondathang dondathang { get; set; }
        public virtual sanpham sanpham { get; set; }
    }
}
