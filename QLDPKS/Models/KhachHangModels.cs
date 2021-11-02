using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLDPKS.Models
{
    public class KhachHangEdit
    {
        public KhachHangEdit()
        {
        }

        public KhachHangEdit(KhachHang n)
        {
            ID = n.ID;
            HoVaTen = n.HoVaTen;
            SDT = n.SDT;
            CMND = n.CMND;
            Email = n.Email;
            DiaChi = n.DiaChi;
            TenDangNhap = n.TenDangNhap;
            MatKhau = n.MatKhau;
            XacNhanMatKhau = n.XacNhanMatKhau;
        }

        [Display(Name = "Mã KH")]
        public int ID { get; set; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Họ và tên không được bỏ trống!")]
        public string HoVaTen { get; set; }

        [Display(Name = "Số Điện Thoại")]
        [Required(ErrorMessage = "Số Điện Thoại không được bỏ trống!")]
        [RegularExpression(@"((09|03|07|08|05)\d{8})$", ErrorMessage = ("Số điện thoại không đúng!"))]
        public string SDT { get; set; }

        [Display(Name = "CMND")]
        [Required(ErrorMessage = "CMND không được bỏ trống!")]
        public string CMND { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email không được bỏ trống!")]
        public string Email { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Họ và tên không được bỏ trống!")]
        public string DiaChi { get; set; }

        [Display(Name = "Tên Đăng Nhập")]
        [Required(ErrorMessage = "Tên Đăng Nhập không được bỏ trống!")]
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

    }
}