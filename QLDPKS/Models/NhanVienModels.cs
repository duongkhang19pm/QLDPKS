using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLDPKS.Models
{
    public class NhanVienEdit
    {
        public NhanVienEdit()
        {
        }

        public NhanVienEdit(NhanVien n)
        {
            ID = n.ID;
            HoVaTenNV = n.HoVaTenNV;
            SDT = n.SDT;
            CMND = n.CMND;
            Email = n.Email;
            DiaChi = n.DiaChi;
            TenDangNhap = n.TenDangNhap;
            MatKhau = n.MatKhau;
            XacNhanMatKhau = n.XacNhanMatKhau;
            Quyen = n.Quyen;
            HinhAnhNV = n.HinhAnhNV;
        }
        [Display(Name = "Mã NV")]
        public int ID { get; set; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Họ và tên không được bỏ trống!")]
        public string HoVaTenNV { get; set; }

        [Display(Name = "Điện thoại")]
        [Required(ErrorMessage = "Điện thoại không được bỏ trống!")]
        public string SDT { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống!")]
        public string DiaChi { get; set; }

        [Display(Name = "CMND")]
        [Required(ErrorMessage = "CMND không được bỏ trống!")]
        public string CMND { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email không được bỏ trống!")]
        public string Email { get; set; }


        [Display(Name = "Hình ảnh nhân viên")]
        public string HinhAnhNV { get; set; }

        [Display(Name = "Hình ảnh nhân viên")]
        public HttpPostedFileBase DuLieuHinhAnhNV { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống!")]
        public string TenDangNhap { get; set; }

        [Display(Name = "Mật Khẩu")]
        [MaxLength(100, ErrorMessage = "Mật khẩu tối đa 100 kí tự")]
        [MinLength(3, ErrorMessage = "Mật khẩu tối thiểu 3 kí tự")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("MatKhau", ErrorMessage = "Xác nhận mật khẩu không chính xác!")]
        [DataType(DataType.Password)]
        public string XacNhanMatKhau { get; set; }

        [Display(Name = "Quyền hạn")]
        [Required(ErrorMessage = "Chưa chọn quyền hạn!")]
        public Nullable<bool> Quyen { get; set; }

    }
    public class NhanVienModelLogin
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống!")]
        public string TenDangNhap { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
    }
    public class DoiMatKhauModelNV
    {
        [Display(Name = "Mật khẩu cũ")]
        [Required(ErrorMessage = "Mật khẩu cũ không được bỏ trống!")]
        [StringLength(100, ErrorMessage = "Mật khẩu cũ chỉ có tối đa 100 kí tự!")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Mật khẩu mới không được bỏ trống!")]
        [StringLength(100, ErrorMessage = "Mật khẩu mới chỉ có tối đa 100 kí tự!")]
        [DataType(DataType.Password)]
        public string MatKhauMoi { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Required(ErrorMessage = "Xác nhận mật khẩu không được bỏ trống!")]
        [StringLength(100, ErrorMessage = "Xác nhận mật khẩu chỉ có tối đa 100 kí tự!")]
        [Compare("MatKhauMoi", ErrorMessage = "Xác nhận mật khẩu không chính xác!")]
        [DataType(DataType.Password)]
        public string XacNhanMatKhau { get; set; }
    }
}
