//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLDPKS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PhieuDatPhong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuDatPhong()
        {
            this.ChiTietPhieuDatPhong = new HashSet<ChiTietPhieuDatPhong>();
        }

        [Display(Name = "Mã Phiếu")]
        public int ID { get; set; }

        [Display(Name = "Khách hàng")]
        public Nullable<int> KhachHang_ID { get; set; }

        [Display(Name = "Nhân viên")]
        public Nullable<int> NhanVien_ID { get; set; }

        [Display(Name = "Số Điện Thoại Liên Hệ ")]
        [Required(ErrorMessage = "Số Điện Thoại Giao Hàng không được bỏ trống!")]
        [RegularExpression(@"((09|03|07|08|05)\d{8})$", ErrorMessage = ("Số điện thoại Giao Hàng không đúng!"))]
        public string SDTLienHe { get; set; }

        [Display(Name = "Ngày đến")]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> NgayDen { get; set; }

        [Display(Name = "Ngày đi")]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> NgayDi { get; set; }

        [Display(Name = "Hình thức thanh toán")]
        public Nullable<short> HinhThucThanhToan { get; set; }

        [Display(Name = "Tình trạng")]
        public Nullable<short> TinhTrang { get; set; }

        public Nullable<System.DateTime> NgayDatPhong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuDatPhong> ChiTietPhieuDatPhong { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}
