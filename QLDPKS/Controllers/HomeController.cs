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
		// GET: Home
		public ActionResult Index()
		{
			return View();
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