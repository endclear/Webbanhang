using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webbanhang.Models
{
    [MetadataTypeAttribute(typeof(thanhvienMetadata))]
    public partial class thanhvien
    {
        internal sealed class thanhvienMetadata
        {
            public int MaTV { get; set; }

            [DisplayName("Tài khoản")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [MinLength(6, ErrorMessage ="{0} phải lớn hơn 6 kí tự")]
            public string TaiKhoan { get; set; }

            [Required(ErrorMessage = "{0} không được để trống")]
            [MinLength(6, ErrorMessage = "{0} phải lớn hơn 6 kí tự")]
            [DisplayName("Mật khẩu")]
            public string MatKhau { get; set; }

            [Required(ErrorMessage = "{0} không được để trống")]
            [DisplayName("Họ tên")]
            public string HoTen { get; set; }

            [DisplayName("Địa chỉ")]
            public string DiaChi { get; set; }

            [DisplayName("Email")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [DataType(DataType.EmailAddress, ErrorMessage = "Định dạng Email không hợp lệ")]
            public string Email { get; set; }

            [DisplayName("Điện thoại")]
            [Required(ErrorMessage ="{0} không được để trống")]
            [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Định dạng không hợp lệ")]
            public string DienThoai { get; set; }
            public string CauHoi { get; set; }
            public string CauTraLoi { get; set; }
            public Nullable<int> MaLoaiTV { get; set; }
        }
    }
}