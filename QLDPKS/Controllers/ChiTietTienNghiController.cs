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
    public class ChiTietTienNghiController : Controller
    {
        private QLKSEntities db = new QLKSEntities();

        // GET: ChiTietTienNghi
        public ActionResult Index()
        {
            var chiTietTienNghi = db.ChiTietTienNghi.Include(c => c.Phong).Include(c => c.TienNghi);
            return View(chiTietTienNghi.ToList());
        }

        // GET: ChiTietTienNghi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietTienNghi chiTietTienNghi = db.ChiTietTienNghi.Find(id);
            if (chiTietTienNghi == null)
            {
                return HttpNotFound();
            }
            return View(chiTietTienNghi);
        }

        // GET: ChiTietTienNghi/Create
        public ActionResult Create()
        {
            ViewBag.Phong_ID = new SelectList(db.Phong, "ID", "TenPhong");
            ViewBag.TienNghi_ID = new SelectList(db.TienNghi, "ID", "TenTienNghi");
            return View();
        }

        // POST: ChiTietTienNghi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TienNghi_ID,Phong_ID")] ChiTietTienNghi chiTietTienNghi)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietTienNghi.Add(chiTietTienNghi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Phong_ID = new SelectList(db.Phong, "ID", "TenPhong", chiTietTienNghi.Phong_ID);
            ViewBag.TienNghi_ID = new SelectList(db.TienNghi, "ID", "TenTienNghi", chiTietTienNghi.TienNghi_ID);
            return View(chiTietTienNghi);
        }

        // GET: ChiTietTienNghi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietTienNghi chiTietTienNghi = db.ChiTietTienNghi.Find(id);
            if (chiTietTienNghi == null)
            {
                return HttpNotFound();
            }
            ViewBag.Phong_ID = new SelectList(db.Phong, "ID", "TenPhong", chiTietTienNghi.Phong_ID);
            ViewBag.TienNghi_ID = new SelectList(db.TienNghi, "ID", "TenTienNghi", chiTietTienNghi.TienNghi_ID);
           
            return View(chiTietTienNghi);
        }

        // POST: ChiTietTienNghi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TienNghi_ID,Phong_ID")] ChiTietTienNghi chiTietTienNghi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietTienNghi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Phong_ID = new SelectList(db.Phong, "ID", "TenPhong", chiTietTienNghi.Phong_ID);
            ViewBag.TienNghi_ID = new SelectList(db.TienNghi, "ID", "TenTienNghi", chiTietTienNghi.TienNghi_ID);
            return View(chiTietTienNghi);
        }

        // GET: ChiTietTienNghi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietTienNghi chiTietTienNghi = db.ChiTietTienNghi.Find(id);
            if (chiTietTienNghi == null)
            {
                return HttpNotFound();
            }
            return View(chiTietTienNghi);
        }

        // POST: ChiTietTienNghi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietTienNghi chiTietTienNghi = db.ChiTietTienNghi.Find(id);
            db.ChiTietTienNghi.Remove(chiTietTienNghi);
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
