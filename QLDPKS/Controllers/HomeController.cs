using QLDPKS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDPKS.Controllers
{
	public class HomeController : Controller
	{
		private QLKSEntities db = new QLKSEntities();
		public ActionResult Index()
		{
			var phong = db.Phong.Where(p => p.TrangThai == 1).ToList();
			return View(phong);
		}
		public ActionResult ChiTiet(int id)
		{
			var phong = db.Phong.Where(p => p.ID == id && p.TrangThai == 1).SingleOrDefault();
			return View(phong);
		}
		[HttpPost]
		public ActionResult Search(FormCollection collection)
		{
			string tukhoa = collection["TuKhoa"].ToString();
			var p = db.Phong.Where(r => r.TrangThai > 0 && r.TenPhong.Contains(tukhoa)).ToList();
			return View(p);
		}
		public ActionResult TinTuc()
		{
			var baiViet = db.BaiViet.Where(p => p.KiemDuyet == 1).OrderByDescending(r => r.NgayDang).ToList();
			return View(baiViet);
		}

		public ActionResult Detail(int id)
		{
			// Lấy thông tin bài viết
			var baiViet = db.BaiViet.Where(p => p.KiemDuyet == 1 && p.ID == id).SingleOrDefault();

			// Tăng lượt xem
			// Chính sách: Mỗi máy chỉ tăng 1 lượt xem
			if (Object.Equals(Session["DaXem" + baiViet.ID], null))
			{
				BaiViet bv = db.BaiViet.Find(id);
				bv.LuotXem = bv.LuotXem + 1;
				db.Entry(bv).State = EntityState.Modified;
				db.SaveChanges();

				// Đánh dấu là đã xem
				Session["DaXem" + bv.ID] = 1;
			}

			// Hiển thị bài viết ra View
			return View(baiViet);
		}
		// GET: NhanVien/Logout
		public ActionResult Logout()
		{
			// Xóa SESSION
			Session.RemoveAll();

			// Quay về trang chủ
			return RedirectToAction("Index", "Home");
		}

		// GET: Home/Login
		public ActionResult Login()
		{
			ModelState.AddModelError("LoginError", "");
			return View();
		}

		// POST: Home/Login
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(KhachHangLogin khachHang)
		{
			if (ModelState.IsValid)
			{
				string matKhauMaHoa = Libs.SHA1.ComputeHash(khachHang.MatKhau);
				var khachhang = db.KhachHang.Where(r => r.TenDangNhap == khachHang.TenDangNhap && r.MatKhau == matKhauMaHoa).SingleOrDefault();

				if (khachhang == null)
				{
					ModelState.AddModelError("LoginError", "Tên đăng nhập hoặc mật khẩu không chính xác!");
					return View(khachHang);
				}
				else
				{

					// Đăng ký SESSION

					Session["MaKhachHang"] = khachhang.ID;
					Session["HoTenKhachHang"] = khachhang.HoVaTen;
					SetAlert("Đăng Nhập Thành Công", "success");
					// Quay về trang chủ
					return RedirectToAction("Index", "Home");

				}
			}

			return View(khachHang);
		}

		public ActionResult NhapThongTin()
		{

			if (Session["MaKhachHang"] == null)
			{
				return RedirectToAction("Login", "Home");
			}
			else
			{
				return View();
			}
		}

		//POST: Register
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult NhapThongTin(PhieuDatPhong phieuDatPhong)
		{
			if (ModelState.IsValid)
			{
				Session["MaPhong"] = phieuDatPhong.ID;
				phieuDatPhong.NgayDatPhong = DateTime.Now;
				phieuDatPhong.TinhTrang = 0;
				phieuDatPhong.KhachHang_ID = Convert.ToInt32(Session["MaKhachHang"]);
				db.PhieuDatPhong.Add(phieuDatPhong);
				db.SaveChanges();
				return RedirectToAction("ThanhToan", "Home", new { id = phieuDatPhong.ID });
			}
			return View(phieuDatPhong);
		}

		// GET: Home/ThanhToan
		public ActionResult ThanhToan(int id)
		{
			if (Session["MaKhachHang"] == null)
			{
				return RedirectToAction("Login", "Home");
			}
			else
			{
				var phieu = db.PhieuDatPhong.Find(id);
				List<DatPhongKS> cart = (List<DatPhongKS>)Session["cart"];
				foreach (var item in cart)
				{
					item.soNgay = (int)(phieu.NgayDi - phieu.NgayDen).Value.TotalDays;
				}
				return View();
			}
		}

		// POST: Home/ThanhToan
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ThanhToan(ChiTietPhieuDatPhong chiTietPhieuDat, int id)
		{
			if (ModelState.IsValid)
			{
				var phieu = db.PhieuDatPhong.Find(id);
				List<DatPhongKS> cart = (List<DatPhongKS>)Session["cart"];
				foreach (var item in cart)
				{
					chiTietPhieuDat.PhieuDatPhong_ID = phieu.ID;
					chiTietPhieuDat.Phong_ID = item.phong.ID;
					chiTietPhieuDat.SoLuong = Convert.ToInt16(item.soNgay);
					chiTietPhieuDat.DonGia = (chiTietPhieuDat.SoLuong * item.phong.Gia);
					db.ChiTietPhieuDatPhong.Add(chiTietPhieuDat);
					db.SaveChanges();
				}
				cart.Clear();
				SetAlert("Thanh Toán Thành Công", "success");
				// Quay về trang chủ
				return RedirectToAction("Index", "Home");
			}
			return View(chiTietPhieuDat);
		}

		//GET: DangKy

		public ActionResult DangKy()
		{
			return View();
		}

		//POST: DangKy
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DangKy(KhachHang kh)
		{
			if (ModelState.IsValid)
			{
				var check = db.KhachHang.FirstOrDefault(r => r.TenDangNhap == kh.TenDangNhap);
				if (check == null)
				{
					kh.MatKhau = Libs.SHA1.ComputeHash(kh.MatKhau);
					kh.XacNhanMatKhau = Libs.SHA1.ComputeHash(kh.XacNhanMatKhau);
					db.Configuration.ValidateOnSaveEnabled = false;
					db.KhachHang.Add(kh);
					db.SaveChanges();
					SetAlert("Đăng Ký Thành Công Vui Lòng Đăng Nhập ", "success");
					return RedirectToAction("Login");
				}
				else
				{
					SetAlert("Tên đăng nhập đã tồn tại vui lòng chọn tên khác !!! ", "warning");
					return View();
				}
			}
			return View();
		}


		public ActionResult DoiMatKhau([Bind(Include = "MatKhau,MatKhauMoi,XacNhanMatKhau")] DoiMatKhauKH doiMatKhauModel)
		{
			if (ModelState.IsValid)
			{
				int makh = Convert.ToInt32(Session["MaKhachHang"]);
				KhachHang khachHang = db.KhachHang.Find(makh);
				if (khachHang == null)
				{
					return HttpNotFound();
				}
				doiMatKhauModel.MatKhau = Libs.SHA1.ComputeHash(doiMatKhauModel.MatKhau);
				if (khachHang.MatKhau != doiMatKhauModel.MatKhau)
				{
					ModelState.AddModelError("MatKhau", "Mật khẩu cũ không chính xác!");
					return View(doiMatKhauModel);
				}
				doiMatKhauModel.MatKhauMoi = Libs.SHA1.ComputeHash(doiMatKhauModel.MatKhauMoi);
				doiMatKhauModel.XacNhanMatKhau = doiMatKhauModel.MatKhauMoi;
				khachHang.MatKhau = doiMatKhauModel.MatKhauMoi;
				khachHang.XacNhanMatKhau = doiMatKhauModel.MatKhauMoi;
				db.Entry(khachHang).State = EntityState.Modified;
				db.SaveChanges();
				SetAlert("Đổi Mật Khẩu Thành Công", "success");
				return RedirectToAction("Index", "Home");
			}
			return View(doiMatKhauModel);
		}



		public ActionResult ChiTietPhieu(int id)
		{

			var phieu = db.PhieuDatPhong.Where(p => p.ID == id).SingleOrDefault();
			return View(phieu);
		}

		public ActionResult ThongTinKH(int id)
		{
			var kh = db.KhachHang.Where(nv => nv.ID == id).SingleOrDefault();
			return View(kh);
		}

		protected void SetAlert(string message, string type)
		{
			TempData["AlertMessage"] = message;
			if (type == "success")
			{
				TempData["AlertType"] = "alert-success";
			}
			else if (type == "warning")
			{
				TempData["AlertType"] = "alert-warning";
			}
			else if (type == "error")
			{
				TempData["AlertType"] = "alert-error";
			}
		}
	}

}