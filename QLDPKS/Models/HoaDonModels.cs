using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDPKS.Models
{
    public class HoaDonModels
    {
        public string HoVaTen { get; set; }
        public string SDT { get; set; }
        public string CMND { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string SDTLienHe { get; set; }
        public Nullable<System.DateTime> NgayDen { get; set; }
        public Nullable<System.DateTime> NgayDi { get; set; }
        public Nullable<short> HinhThucThanhToan { get; set; }
        public Nullable<System.DateTime> NgayDatPhong { get; set; }
        public string TenPhong { get; set; }
        public Nullable<long> Gia { get; set; }
        public Nullable<int> SLNguoi { get; set; }
        public string HinhAnhBia { get; set; }
        public string DTPhong { get; set; }
        public string TenLoai { get; set; }
        public Nullable<short> SoNgay { get; set; }
        public Nullable<long> ThanhTien { get; set; }
    }
}