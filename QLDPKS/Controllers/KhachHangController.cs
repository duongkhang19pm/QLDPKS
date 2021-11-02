using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLDPKS.Models;

namespace QLDPKS.Controllers
{
    public class KhachHangController : Controller
    {
        private QLKSEntities db = new QLKSEntities();

        // GET: Admin/KhachHang
        public ActionResult Index()
        {
            return View(db.KhachHang.ToList());
        }



        // GET: Admin/KhachHang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KhachHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HoVaTen,SDT,CMND,Email,DiaChi,TenDangNhap,MatKhau,XacNhanMatKhau")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                Session["MaKhachHang"] = khachHang.ID;
                Session["HoTenKH"] = khachHang.HoVaTen;
                khachHang.MatKhau = Libs.SHA1.ComputeHash(khachHang.MatKhau);
                khachHang.XacNhanMatKhau = Libs.SHA1.ComputeHash(khachHang.XacNhanMatKhau);
                db.KhachHang.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khachHang);
        }

        // GET: Admin/KhachHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHang.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(new KhachHangEdit(khachHang));
        }

        // POST: Admin/KhachHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HoVaTen,SDT,CMND,Email,DiaChi,TenDangNhap,MatKhau,XacNhanMatKhau")] KhachHangEdit khachHang)
        {
            if (ModelState.IsValid)
            {
                KhachHang k = db.KhachHang.Find(khachHang.ID);

                // Giữ nguyên mật khẩu cũ
                if (khachHang.MatKhau == null)
                {
                    k.ID = khachHang.ID;
                    k.HoVaTen = khachHang.HoVaTen;
                    k.SDT = khachHang.SDT;
                    k.DiaChi = khachHang.DiaChi;
                    k.CMND = khachHang.CMND;
                    k.Email = khachHang.Email;
                    k.TenDangNhap = khachHang.TenDangNhap;
                    k.XacNhanMatKhau = k.MatKhau;
                }
                else // Cập nhật mật khẩu mới
                {
                    k.ID = khachHang.ID;
                    k.HoVaTen = khachHang.HoVaTen;
                    k.SDT = khachHang.SDT;
                    k.DiaChi = khachHang.DiaChi;
                    k.CMND = khachHang.CMND;
                    k.Email = khachHang.Email;
                    k.TenDangNhap = khachHang.TenDangNhap;
                    k.MatKhau = Libs.SHA1.ComputeHash(khachHang.MatKhau);
                    k.XacNhanMatKhau = Libs.SHA1.ComputeHash(khachHang.XacNhanMatKhau);

                }
                db.Entry(k).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khachHang);
        }

        // GET: Admin/KhachHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHang.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: Admin/KhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachHang khachHang = db.KhachHang.Find(id);
            db.KhachHang.Remove(khachHang);
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
