﻿//------------------------------------------------------------------------------
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
    using System.Web;

    public partial class Phong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Phong()
        {
            this.AlbumAnh = new HashSet<AlbumAnh>();
            this.ChiTietDichVu = new HashSet<ChiTietDichVu>();
            this.ChiTietPhieuDatPhong = new HashSet<ChiTietPhieuDatPhong>();
            this.ChiTietTienNghi = new HashSet<ChiTietTienNghi>();
        }

        [Display(Name = "Mã Phòng")]
        public int ID { get; set; }

        [Display(Name = "Loại phòng")]
        [Required(ErrorMessage = "Chưa chọn loại phòng!")]
        public Nullable<int> LoaiPhong_ID { get; set; }

        [Display(Name = "Tên phòng")]
        [Required(ErrorMessage = "Tên phòng không được bỏ trống!")]
        public string TenPhong { get; set; }

        [Display(Name = "Đơn giá")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "Đơn giá bản không được bỏ trống!")]
        public Nullable<long> Gia { get; set; }

        [Display(Name = "Số lượng Người")]
        [Required(ErrorMessage = "Số lượng người không được bỏ trống!")]
        public Nullable<int> SLNguoi { get; set; }

        [Display(Name = "Hình ảnh bìa")]
        public string HinhAnhBia { get; set; }

        [Display(Name = "Hình ảnh bìa")]
        public HttpPostedFileBase DuLieuHinhAnhBia { get; set; }

        [Display(Name = "Diện Tích")]
        [Required(ErrorMessage = "Diện tích không được bỏ trống!")]
        public string DTPhong { get; set; }

        [Display(Name = "Mô tả")]
        [DataType(DataType.MultilineText)]
        public string MoTa { get; set; }

        [Display(Name = "Trạng Thái")]
        public Nullable<byte> TrangThai { get; set; }

        public List<int> ChiTietDV { get; set; }
        public List<int> ChiTietTN { get; set; }
        public string AlbumHinhAnh { get; set; }
        public IEnumerable<HttpPostedFileBase> DuLieuAlbumAnh { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlbumAnh> AlbumAnh { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDichVu> ChiTietDichVu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuDatPhong> ChiTietPhieuDatPhong { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietTienNghi> ChiTietTienNghi { get; set; }
        public virtual LoaiPhong LoaiPhong { get; set; }
    }
}
