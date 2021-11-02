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
    public class ChiTietDichVuController : Controller
    {
        private QLKSEntities db = new QLKSEntities();

        // GET: Admin/ChiTietDichVu
        public ActionResult Index()
        {
            var chiTietDichVu = db.ChiTietDichVu.Include(c => c.DichVu).Include(c => c.Phong);
            return View(chiTietDichVu.ToList());
        }

        // GET: Admin/ChiTietDichVu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDichVu chiTietDichVu = db.ChiTietDichVu.Find(id);
            if (chiTietDichVu == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDichVu);
        }

        // GET: Admin/ChiTietDichVu/Create
        public ActionResult Create()
        {
            ViewBag.DichVu_ID = new SelectList(db.DichVu, "ID", "TenDichVu");
            ViewBag.Phong_ID = new SelectList(db.Phong, "ID", "TenPhong");
            return View();
        }

        // POST: Admin/ChiTietDichVu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DichVu_ID,Phong_ID")] ChiTietDichVu chiTietDichVu)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietDichVu.Add(chiTietDichVu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DichVu_ID = new SelectList(db.DichVu, "ID", "TenDichVu", chiTietDichVu.DichVu_ID);
            ViewBag.Phong_ID = new SelectList(db.Phong, "ID", "TenPhong", chiTietDichVu.Phong_ID);
            return View(chiTietDichVu);
        }

        // GET: Admin/ChiTietDichVu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDichVu chiTietDichVu = db.ChiTietDichVu.Find(id);
            if (chiTietDichVu == null)
            {
                return HttpNotFound();
            }
            ViewBag.DichVu_ID = new SelectList(db.DichVu, "ID", "TenDichVu", chiTietDichVu.DichVu_ID);
            ViewBag.Phong_ID = new SelectList(db.Phong, "ID", "TenPhong", chiTietDichVu.Phong_ID);
            return View(chiTietDichVu);
        }

        // POST: Admin/ChiTietDichVu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DichVu_ID,Phong_ID")] ChiTietDichVu chiTietDichVu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDichVu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DichVu_ID = new SelectList(db.DichVu, "ID", "TenDichVu", chiTietDichVu.DichVu_ID);
            ViewBag.Phong_ID = new SelectList(db.Phong, "ID", "TenPhong", chiTietDichVu.Phong_ID);
            return View(chiTietDichVu);
        }

        // GET: Admin/ChiTietDichVu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDichVu chiTietDichVu = db.ChiTietDichVu.Find(id);
            if (chiTietDichVu == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDichVu);
        }

        // POST: Admin/ChiTietDichVu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietDichVu chiTietDichVu = db.ChiTietDichVu.Find(id);
            db.ChiTietDichVu.Remove(chiTietDichVu);
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
