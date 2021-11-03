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
    public class PhongController : Controller
    {
        private QLKSEntities db = new QLKSEntities();

        // GET: Phong
        public ActionResult Index()
        {
            var phong = db.Phong.Include(p => p.LoaiPhong);
            return View(phong.ToList());
        }

        public ActionResult ChiTiet(int id)
        {
            var phong = db.Phong.Where(p => p.ID == id).SingleOrDefault();
            return View(phong);
        }
        // GET: Admin/Phong/Approved/1
        public ActionResult Approved(int id)
        {
            Phong bv = db.Phong.Find(id);
            bv.TrangThai = System.Convert.ToByte(1 - bv.TrangThai); // 1 -> 0 và 0 -> 1
            db.Entry(bv).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        // GET: Phong/Create
        public ActionResult Create()
        {
            ViewBag.DichVu_ID = new SelectList(db.DichVu, "ID", "TenDichVu");
            ViewBag.TienNghi_ID = new SelectList(db.TienNghi, "ID", "TenTienNghi");
            ViewBag.LoaiPhong_ID = new SelectList(db.LoaiPhong, "ID", "TenLoai");
            ModelState.AddModelError("UploadError", "");
            return View();
        }

        // POST: Phong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,LoaiPhong_ID,TenPhong,Gia,SLNguoi,DuLieuHinhAnhBia,DTPhong,MoTa,TrangThai,ChiTietDV,ChiTietTN,DuLieuAlbumAnh")] Phong phong)
        {

            ViewBag.LoaiPhong_ID = new SelectList(db.LoaiPhong, "ID", "TenLoai", phong.LoaiPhong_ID);
            ViewBag.DichVu_ID = new SelectList(db.DichVu, "ID", "TenDichVu", phong.ChiTietDichVu);
            ViewBag.TienNghi_ID = new SelectList(db.TienNghi, "ID", "TenTienNghi", phong.ChiTietTienNghi);
            if (ModelState.IsValid)
            {

                phong.TrangThai = 1;

                // Upload ảnh 
                if (phong.DuLieuHinhAnhBia != null)
                {

                    string folder = "Storage/";
                    string fileExtension = Path.GetExtension(phong.DuLieuHinhAnhBia.FileName).ToLower();

                    // Kiểm tra kiểu
                    var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    if (!fileTypeSupported.Contains(fileExtension))
                    {
                        ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                        return View(phong);
                    }
                    else if (phong.DuLieuHinhAnhBia.ContentLength > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                        return View(phong);
                    }
                    else
                    {
                        string fileName = Guid.NewGuid().ToString() + fileExtension;
                        string filePath = Path.Combine(Server.MapPath("~/" + folder), fileName);
                        phong.DuLieuHinhAnhBia.SaveAs(filePath);

                        // Cập nhật đường dẫn vào CSDL
                        phong.HinhAnhBia = folder + fileName;

                        db.Phong.Add(phong);
                        db.SaveChanges();

                    }
                }
                else
                {
                    ModelState.AddModelError("UploadError", "Hình ảnh bìa không được bỏ trống!");
                    return View(phong);
                }

                // them chi tiet dich vu
                foreach (var item in phong.ChiTietDV)
                {
                    if (item > 0)
                    {
                        var location = new ChiTietDichVu
                        {
                            DichVu_ID = item,
                            Phong_ID = phong.ID
                        };
                        db.ChiTietDichVu.Add(location);
                        db.SaveChanges();
                    }

                }
                // them chi tiet tiennghi
                foreach (var tn in phong.ChiTietTN)
                {
                    if (tn > 0)
                    {
                        var location = new ChiTietTienNghi
                        {
                            TienNghi_ID = tn,
                            Phong_ID = phong.ID
                        };
                        db.ChiTietTienNghi.Add(location);
                        db.SaveChanges();
                    }

                }
                string folder2 = "/AlbunmAnh";
                foreach (var dc in phong.DuLieuAlbumAnh)
                {

                    string fileExtension = Path.GetExtension(dc.FileName).ToLower();

                    string fileName = Guid.NewGuid().ToString() + fileExtension;
                    var image = folder2 + fileName;
                    string filePath = Path.Combine(Server.MapPath(image));


                    if (dc != null && dc.ContentLength > 0)
                    {
                        dc.SaveAs(filePath);
                        var images = new AlbumAnh
                        {
                            AlbumHinhAnh = image,
                            Phong_ID = phong.ID
                        };
                        db.AlbumAnh.Add(images);
                        db.SaveChanges();
                    }
                }


                return RedirectToAction("Index");
            }


            return View(phong);
        }

        // GET: Phong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phong phong = db.Phong.Find(id);
            if (phong == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiPhong_ID = new SelectList(db.LoaiPhong, "ID", "TenLoai", phong.LoaiPhong_ID);
            ModelState.AddModelError("UploadError", "");
            return View(phong);
        }

        // POST: Phong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,HinhAnhBia,LoaiPhong_ID,TenPhong,Gia,SLNguoi,DuLieuHinhAnhBia,DTPhong,MoTa,TrangThai,ChiTietDV,ChiTietTN,DuLieuAlbumAnh")] Phong phong)
        {

            ViewBag.LoaiPhong_ID = new SelectList(db.LoaiPhong, "ID", "TenLoai", phong.LoaiPhong_ID);
            if (ModelState.IsValid)
            {


                // Upload ảnh 
                if (phong.DuLieuHinhAnhBia != null)
                {

                    string folder = "Storage/";
                    string fileExtension = Path.GetExtension(phong.DuLieuHinhAnhBia.FileName).ToLower();

                    // Kiểm tra kiểu
                    var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    if (!fileTypeSupported.Contains(fileExtension))
                    {
                        ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin JPG, PNG, GIF!");
                        return View(phong);
                    }
                    else if (phong.DuLieuHinhAnhBia.ContentLength > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin từ 2MB trở xuống!");
                        return View(phong);
                    }
                    else
                    {
                        string fileName = Guid.NewGuid().ToString() + fileExtension;
                        string filePath = Path.Combine(Server.MapPath("~/" + folder), fileName);
                        phong.DuLieuHinhAnhBia.SaveAs(filePath);

                        // Cập nhật đường dẫn vào CSDL
                        phong.HinhAnhBia = folder + fileName;

                        db.Phong.Add(phong);
                        db.SaveChanges();
                        return RedirectToAction("Index");

                    }
                }
                else
                {
                    db.Entry(phong).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }


            }

            return View(phong);
        }

        // GET: Phong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phong phong = db.Phong.Find(id);
            if (phong == null)
            {
                return HttpNotFound();
            }
            return View(phong);
        }

        // POST: Phong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phong phong = db.Phong.Find(id);
            db.Phong.Remove(phong);
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
