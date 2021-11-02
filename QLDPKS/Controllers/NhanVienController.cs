using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLDPKS.Models;

namespace QLDPKS.Controllers
{
    public class NhanVienController : Controller
    {
        private QLKSEntities db = new QLKSEntities();

        // GET: NhanVien
        public ActionResult Index()
        {
            return View(db.NhanVien.ToList());
        }
        // GET: NhanVien/Create
        public ActionResult Create()
        {

            ModelState.AddModelError("UploadError", "");
            return View();
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HoVaTenNV,SDT,DiaChi,CMND,Email,DuLieuHinhAnhNV,TenDangNhap,MatKhau,XacNhanMatKhau,Quyen")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {// Upload ảnh 
                if (nhanVien.DuLieuHinhAnhNV != null)
                {
                    string folder = "AnhNhanVien/";
                    string fileExtension = Path.GetExtension(nhanVien.DuLieuHinhAnhNV.FileName).ToLower();

                    // Kiểm tra kiểu
                    var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    if (!fileTypeSupported.Contains(fileExtension))
                    {
                        ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                        return View(nhanVien);
                    }
                    else if (nhanVien.DuLieuHinhAnhNV.ContentLength > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                        return View(nhanVien);
                    }
                    else
                    {
                        string fileName = Guid.NewGuid().ToString() + fileExtension;
                        string filePath = Path.Combine(Server.MapPath("~/" + folder), fileName);
                        nhanVien.DuLieuHinhAnhNV.SaveAs(filePath);

                        // Cập nhật đường dẫn vào CSDL
                        nhanVien.HinhAnhNV = folder + fileName;


                        // Mã hóa mật khẩu
                        nhanVien.MatKhau = Libs.SHA1.ComputeHash(nhanVien.MatKhau);
                        nhanVien.XacNhanMatKhau = Libs.SHA1.ComputeHash(nhanVien.XacNhanMatKhau);

                        db.NhanVien.Add(nhanVien);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("UploadError", "Hình ảnh đại diện nhân viên không được bỏ trống!");
                    return View(nhanVien);
                }
            }
                return View(nhanVien);
        }

        // GET: NhanVien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            ModelState.AddModelError("UploadError", "");
            return View(new NhanVienEdit(nhanVien));
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, HinhAnhNV, HoVaTenNV, SDT, DiaChi, CMND, Email, DuLieuHinhAnhNV, TenDangNhap, MatKhau, XacNhanMatKhau, Quyen")] NhanVienEdit nhanVien)
        {
            if (ModelState.IsValid)
            {
                NhanVien n = db.NhanVien.Find(nhanVien.ID);

                // Upload ảnh mới
                if (nhanVien.DuLieuHinhAnhNV != null)
                {
                    // Xóa ảnh cũ
                    string oldFilePath = Server.MapPath("~/" + nhanVien.HinhAnhNV);
                    if (System.IO.File.Exists(oldFilePath)) System.IO.File.Delete(oldFilePath);

                    string folder = "AnhNhanVien/";
                    string fileExtension = Path.GetExtension(nhanVien.DuLieuHinhAnhNV.FileName).ToLower();

                    // Kiểm tra kiểu
                    var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    if (!fileTypeSupported.Contains(fileExtension))
                    {
                        ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                        return View(nhanVien);
                    }
                    else if (nhanVien.DuLieuHinhAnhNV.ContentLength > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                        return View(nhanVien);
                    }
                    else
                    {
                        string fileName = Guid.NewGuid().ToString() + fileExtension;
                        string filePath = Path.Combine(Server.MapPath("~/" + folder), fileName);
                        nhanVien.DuLieuHinhAnhNV.SaveAs(filePath);

                        // Cập nhật đường dẫn vào CSDL
                        nhanVien.HinhAnhNV = folder + fileName;

                        // Giữ nguyên mật khẩu cũ
                        if (nhanVien.MatKhau == null)
                        {
                            n.ID = nhanVien.ID;
                            n.HoVaTenNV = nhanVien.HoVaTenNV;
                            n.SDT = nhanVien.SDT;
                            n.DiaChi = nhanVien.DiaChi;
                            n.CMND = nhanVien.CMND;
                            n.Email = nhanVien.Email;
                            n.TenDangNhap = nhanVien.TenDangNhap;
                            n.XacNhanMatKhau = n.MatKhau;
                            n.HinhAnhNV = nhanVien.HinhAnhNV;
                            n.Quyen = nhanVien.Quyen;
                            db.Entry(n).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else // Cập nhật mật khẩu mới
                        {
                            n.ID = nhanVien.ID;
                            n.HoVaTenNV = nhanVien.HoVaTenNV;
                            n.SDT = nhanVien.SDT;
                            n.DiaChi = nhanVien.DiaChi;
                            n.CMND = nhanVien.CMND;
                            n.Email = nhanVien.Email;
                            n.TenDangNhap = nhanVien.TenDangNhap;
                            n.MatKhau = Libs.SHA1.ComputeHash(nhanVien.MatKhau);
                            n.XacNhanMatKhau = Libs.SHA1.ComputeHash(nhanVien.XacNhanMatKhau);
                            n.HinhAnhNV = nhanVien.HinhAnhNV;
                            n.Quyen = nhanVien.Quyen;
                            db.Entry(n).State = EntityState.Modified;
                            db.SaveChanges();

                        }

                        return RedirectToAction("Index");

                    }
                }
                else // Giữ nguyên ảnh cũ
                {
                    // Giữ nguyên mật khẩu cũ
                    if (nhanVien.MatKhau == null)
                    {
                        n.ID = nhanVien.ID;
                        n.HoVaTenNV = nhanVien.HoVaTenNV;
                        n.SDT = nhanVien.SDT;
                        n.DiaChi = nhanVien.DiaChi;
                        n.CMND = nhanVien.CMND;
                        n.Email = nhanVien.Email;
                        n.TenDangNhap = nhanVien.TenDangNhap;
                        n.XacNhanMatKhau = n.MatKhau;
                        n.HinhAnhNV = nhanVien.HinhAnhNV;
                        n.Quyen = nhanVien.Quyen;
                        db.Entry(n).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else // Cập nhật mật khẩu mới
                    {
                        n.ID = nhanVien.ID;
                        n.HoVaTenNV = nhanVien.HoVaTenNV;
                        n.SDT = nhanVien.SDT;
                        n.DiaChi = nhanVien.DiaChi;
                        n.CMND = nhanVien.CMND;
                        n.Email = nhanVien.Email;
                        n.TenDangNhap = nhanVien.TenDangNhap;
                        n.MatKhau = Libs.SHA1.ComputeHash(nhanVien.MatKhau);
                        n.XacNhanMatKhau = Libs.SHA1.ComputeHash(nhanVien.XacNhanMatKhau);
                        n.HinhAnhNV = nhanVien.HinhAnhNV;
                        n.Quyen = nhanVien.Quyen;
                        db.Entry(n).State = EntityState.Modified;
                        db.SaveChanges();

                    }

                    return RedirectToAction("Index");
                }

            }
            return View(nhanVien);
        }

        // GET: NhanVien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NhanVien nhanVien = db.NhanVien.Find(id);
            db.NhanVien.Remove(nhanVien);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
