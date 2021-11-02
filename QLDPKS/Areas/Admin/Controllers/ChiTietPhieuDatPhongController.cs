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
    public class ChiTietPhieuDatPhongController : Controller
    {
        private QLKSEntities db = new QLKSEntities();

        // GET: Admin/ChiTietPhieuDatPhong
        public ActionResult Index()
        {
            var chiTietPhieuDatPhong = db.ChiTietPhieuDatPhong.Include(c => c.PhieuDatPhong).Include(c => c.Phong);
            return View(chiTietPhieuDatPhong.ToList());
        }

        // GET: Admin/ChiTietPhieuDatPhong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuDatPhong chiTietPhieuDatPhong = db.ChiTietPhieuDatPhong.Find(id);
            if (chiTietPhieuDatPhong == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPhieuDatPhong);
        }

        // GET: Admin/ChiTietPhieuDatPhong/Create
        public ActionResult Create()
        {
            ViewBag.PhieuDatPhong_ID = new SelectList(db.PhieuDatPhong, "ID", "SDTLienHe");
            ViewBag.Phong_ID = new SelectList(db.Phong, "ID", "TenPhong");
            return View();
        }

        // POST: Admin/ChiTietPhieuDatPhong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PhieuDatPhong_ID,Phong_ID,SoLuong,DonGia")] ChiTietPhieuDatPhong chiTietPhieuDatPhong)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietPhieuDatPhong.Add(chiTietPhieuDatPhong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PhieuDatPhong_ID = new SelectList(db.PhieuDatPhong, "ID", "SDTLienHe", chiTietPhieuDatPhong.PhieuDatPhong_ID);
            ViewBag.Phong_ID = new SelectList(db.Phong, "ID", "TenPhong", chiTietPhieuDatPhong.Phong_ID);
            return View(chiTietPhieuDatPhong);
        }

        // GET: Admin/ChiTietPhieuDatPhong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuDatPhong chiTietPhieuDatPhong = db.ChiTietPhieuDatPhong.Find(id);
            if (chiTietPhieuDatPhong == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhieuDatPhong_ID = new SelectList(db.PhieuDatPhong, "ID", "SDTLienHe", chiTietPhieuDatPhong.PhieuDatPhong_ID);
            ViewBag.Phong_ID = new SelectList(db.Phong, "ID", "TenPhong", chiTietPhieuDatPhong.Phong_ID);
            return View(chiTietPhieuDatPhong);
        }

        // POST: Admin/ChiTietPhieuDatPhong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PhieuDatPhong_ID,Phong_ID,SoLuong,DonGia")] ChiTietPhieuDatPhong chiTietPhieuDatPhong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietPhieuDatPhong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PhieuDatPhong_ID = new SelectList(db.PhieuDatPhong, "ID", "SDTLienHe", chiTietPhieuDatPhong.PhieuDatPhong_ID);
            ViewBag.Phong_ID = new SelectList(db.Phong, "ID", "TenPhong", chiTietPhieuDatPhong.Phong_ID);
            return View(chiTietPhieuDatPhong);
        }

        // GET: Admin/ChiTietPhieuDatPhong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuDatPhong chiTietPhieuDatPhong = db.ChiTietPhieuDatPhong.Find(id);
            if (chiTietPhieuDatPhong == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPhieuDatPhong);
        }

        // POST: Admin/ChiTietPhieuDatPhong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietPhieuDatPhong chiTietPhieuDatPhong = db.ChiTietPhieuDatPhong.Find(id);
            db.ChiTietPhieuDatPhong.Remove(chiTietPhieuDatPhong);
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
