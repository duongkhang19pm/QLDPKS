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
    
    public partial class PhieuDatPhong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuDatPhong()
        {
            this.ChiTietPhieuDatPhong = new HashSet<ChiTietPhieuDatPhong>();
        }
    
        public int ID { get; set; }
        public Nullable<int> KhachHang_ID { get; set; }
        public Nullable<int> NhanVien_ID { get; set; }
        public string SDTLienHe { get; set; }
        public Nullable<System.DateTime> NgayDen { get; set; }
        public Nullable<System.DateTime> NgayDi { get; set; }
        public Nullable<short> HinhThucThanhToan { get; set; }
        public Nullable<short> TinhTrang { get; set; }
        public Nullable<System.DateTime> NgayDatPhong { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuDatPhong> ChiTietPhieuDatPhong { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}
