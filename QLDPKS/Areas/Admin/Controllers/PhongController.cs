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
    public class PhongController : Controller
    {
        private QLKSEntities db = new QLKSEntities();

        // GET: Admin/Phong
        public ActionResult Index()
        {
            var phong = db.Phong.Include(p => p.LoaiPhong);
            return View(phong.ToList());
        }

        // GET: Admin/Phong/Details/5
        public ActionResult Details(int? id)
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

        // GET: Admin/Phong/Create
        public ActionResult Create()
        {
            ViewBag.LoaiPhong_ID = new SelectList(db.LoaiPhong, "ID", "TenLoai");
            return View();
        }

        // POST: Admin/Phong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LoaiPhong_ID,TenPhong,Gia,SLNguoi,HinhAnhBia,DTPhong,MoTa,TrangThai")] Phong phong)
        {
            if (ModelState.IsValid)
            {
                db.Phong.Add(phong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoaiPhong_ID = new SelectList(db.LoaiPhong, "ID", "TenLoai", phong.LoaiPhong_ID);
            return View(phong);
        }

        // GET: Admin/Phong/Edit/5
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
            return View(phong);
        }

        // POST: Admin/Phong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LoaiPhong_ID,TenPhong,Gia,SLNguoi,HinhAnhBia,DTPhong,MoTa,TrangThai")] Phong phong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiPhong_ID = new SelectList(db.LoaiPhong, "ID", "TenLoai", phong.LoaiPhong_ID);
            return View(phong);
        }

        // GET: Admin/Phong/Delete/5
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

        // POST: Admin/Phong/Delete/5
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
