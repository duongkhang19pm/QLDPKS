using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLDPKS.Models;

namespace QLDPKS.Areas.Admin.Controllers
{
    public class BinhLuanController : Controller
    {
        private QLKSEntities db = new QLKSEntities();

        // GET: Admin/BinhLuan
        public ActionResult Index()
        {
            var binhLuan = db.BinhLuan.Include(b => b.BaiViet).Include(b => b.KhachHang);
            return View(binhLuan.ToList());
        }

        // GET: Admin/BaiViet/Approved/1
        public ActionResult Approved(int id)
        {
            BinhLuan bv = db.BinhLuan.Find(id);
            bv.KiemDuyet = System.Convert.ToByte(1 - bv.KiemDuyet); // 1 -> 0 và 0 -> 1
            db.Entry(bv).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        // GET: Admin/BinhLuan/Create
        public ActionResult Create()
        {
            ViewBag.BaiViet_ID = new SelectList(db.BaiViet, "ID", "TieuDe");
            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaTen");
            return View();
        }

        // POST: Admin/BinhLuan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,BaiViet_ID,KhachHang_ID,NoiDung,NgayDang,KiemDuyet")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                //Bổ sung các trường còn thiếu
                //binhLuan.KhachHang_ID = Convert.ToInt32(Session["MaKhachHang"]);
                binhLuan.NgayDang = DateTime.Now;
                binhLuan.KiemDuyet = binhLuan.KiemDuyet = 0;

                db.BinhLuan.Add(binhLuan);
                db.SaveChanges();
               
              return RedirectToAction("Index", "Home", new { id = binhLuan.BaiViet_ID, area = "" });
            }

            ViewBag.BaiViet_ID = new SelectList(db.BaiViet, "ID", "TieuDe", binhLuan.BaiViet_ID);
            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaTen", binhLuan.KhachHang_ID);
            return View(binhLuan);
        }

        // GET: Admin/BinhLuan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuan.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            ViewBag.BaiViet_ID = new SelectList(db.BaiViet, "ID", "TieuDe", binhLuan.BaiViet_ID);
            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaTen", binhLuan.KhachHang_ID);
            return View(binhLuan);
        }

        // POST: Admin/BinhLuan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,BaiViet_ID,KhachHang_ID,NoiDung,NgayDang,KiemDuyet")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(binhLuan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BaiViet_ID = new SelectList(db.BaiViet, "ID", "TieuDe", binhLuan.BaiViet_ID);
            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaTen", binhLuan.KhachHang_ID);
            return View(binhLuan);
        }

        // GET: Admin/BinhLuan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuan.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            return View(binhLuan);
        }

        // POST: Admin/BinhLuan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BinhLuan binhLuan = db.BinhLuan.Find(id);
            db.BinhLuan.Remove(binhLuan);
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
