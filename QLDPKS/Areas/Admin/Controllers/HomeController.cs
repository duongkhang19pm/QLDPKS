using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLDPKS.Models;
namespace QLDPKS.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        QLKSEntities db = new QLKSEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            // Lấy thông tin bài viết
            var baiViet = db.BaiViet.Where(r => r.KiemDuyet == 1 && r.ID == id).SingleOrDefault();

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
			ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HoVaTenNV");
			return View();
		}

		// POST: Home/Login
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(NhanVienLogin nhanVien)
		{
			if (ModelState.IsValid)
			{
				string matKhauMaHoa = Libs.SHA1.ComputeHash(nhanVien.MatKhau);
				var nhanvien = db.NhanVien.Where(r => r.TenDangNhap == nhanVien.TenDangNhap && r.MatKhau == matKhauMaHoa).SingleOrDefault();

				if (nhanvien == null)
				{

					ModelState.AddModelError("LoginError", "Tên đăng nhập hoặc mật khẩu không chính xác!");
					return View(nhanVien);
				}
				else
				{
					// Đăng ký SESSION

					Session["MaNhanVien"] = nhanvien.ID;
					Session["HoTenNhanVien"] = nhanvien.HoVaTenNV;
					Session["CMND"] = nhanvien.CMND;
					Session["DiaChi"] = nhanvien.DiaChi;
					Session["Email"] = nhanvien.Email;
					Session["SDT"] = nhanvien.SDT;
					Session["Quyen"] = nhanvien.Quyen;
					Session["HinhAnhNV"] = nhanvien.HinhAnhNV;

					SetAlert("Đăng Nhập Thành Công", "success");
					// Quay về trang chủ
					return RedirectToAction("Index", "Home");
				}
			}

			return View(nhanVien);
		}



		public ActionResult DoiMatKhau([Bind(Include = "MatKhau,MatKhauMoi,XacNhanMatKhau")] DoiMatKhauNV doiMatKhauModel)
		{
			if (ModelState.IsValid)
			{
				int manv = Convert.ToInt32(Session["MaNhanVien"]);
				NhanVien nhanVien = db.NhanVien.Find(manv);
				if (nhanVien == null)
				{
					return HttpNotFound();
				}
				doiMatKhauModel.MatKhau = Libs.SHA1.ComputeHash(doiMatKhauModel.MatKhau);
				if (nhanVien.MatKhau != doiMatKhauModel.MatKhau)
				{
					ModelState.AddModelError("MatKhau", "Mật khẩu cũ không chính xác!");
					return View(doiMatKhauModel);
				}
				doiMatKhauModel.MatKhauMoi = Libs.SHA1.ComputeHash(doiMatKhauModel.MatKhauMoi);
				doiMatKhauModel.XacNhanMatKhau = doiMatKhauModel.MatKhauMoi;
				nhanVien.MatKhau = doiMatKhauModel.MatKhauMoi;
				nhanVien.XacNhanMatKhau = doiMatKhauModel.MatKhauMoi;
				db.Entry(nhanVien).State = EntityState.Modified;
				db.SaveChanges();
				//SetAlert("Đổi Mật Khẩu Thành Công", "success");
				return RedirectToAction("Index", "Home");
			}
			return View(doiMatKhauModel);
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
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