using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webbanhang.Models
{
    [MetadataTypeAttribute(typeof(sanphamMetadata))]
    public partial class sanpham
    {
        internal sealed class sanphamMetadata {
            [DisplayName("Mã SP")]
            public int MaSP { get; set; }

            [DisplayName("Tên SP")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [MinLength(6, ErrorMessage = "{0} phải lớn hơn 6 kí tự")]
            public string TenSP { get; set; }

            [DisplayName("Đơn giá")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public Nullable<decimal> DonGia { get; set; }

            [DisplayName("Ngày cập nhật")]
            public Nullable<System.DateTime> NgayCapNhat { get; set; }

            [DisplayName("Cấu hình")]
            public string CauHinh { get; set; }

            [DisplayName("Mô tả")]
            public string MoTa { get; set; }

            [DisplayName("Hình ảnh")]
            public string HinhAnh { get; set; }

            [DisplayName("Số lượng tồn")]
            public Nullable<int> SoLuongTon { get; set; }

            [DisplayName("Lượt xem")]
            public Nullable<int> LuotXem { get; set; }

            [DisplayName("Lượt bình chọn")]
            public Nullable<int> LuotBinhChon { get; set; }

            [DisplayName("Lượt bình luận")]
            public Nullable<int> LuotBinhLuan { get; set; }

            [DisplayName("Số lần mua")]
            public Nullable<int> SoLanMua { get; set; }
            public Nullable<int> Moi { get; set; }

            [DisplayName("Nhà cung cấp")]
            public Nullable<int> MaNCC { get; set; }

            [DisplayName("Nhà sản xuất")]
            public Nullable<int> MaNSX { get; set; }

            [DisplayName("Loại sản phẩm")]
            public Nullable<int> MaLoai { get; set; }

            public Nullable<bool> DaXoa { get; set; }

            [DisplayName("Bí danh")]
            public string Bidanh { get; set; }

            [DisplayName("Hình ảnh 1")]
            public string HinhAnh1 { get; set; }

            [DisplayName("Hình ảnh 2")]
            public string HinhAnh2 { get; set; }

            [DisplayName("Hình ảnh 3")]
            public string HinhAnh3 { get; set; }

            [DisplayName("Hình ảnh 4")]
            public string HinhAnh4 { get; set; }
        }
    }
}