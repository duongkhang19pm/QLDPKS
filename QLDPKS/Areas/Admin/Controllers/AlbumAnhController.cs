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
    public class AlbumAnhController : Controller
    {
        private QLKSEntities db = new QLKSEntities();

        // GET: AlbumAnh
        public ActionResult Index()
        {
            var albumAnh = db.AlbumAnh.Include(a => a.Phong);
            return View(albumAnh.ToList());
        }

        // GET: AlbumAnh/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumAnh albumAnh = db.AlbumAnh.Find(id);
            if (albumAnh == null)
            {
                return HttpNotFound();
            }
            return View(albumAnh);
        }

        // GET: AlbumAnh/Create
        public ActionResult Create()
        {
            ViewBag.Phong_ID = new SelectList(db.Phong, "ID", "TenPhong");
            return View();
        }

        // POST: AlbumAnh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Phong_ID,AlbumHinhAnh")] AlbumAnh albumAnh)
        {
            if (ModelState.IsValid)
            {
                db.AlbumAnh.Add(albumAnh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Phong_ID = new SelectList(db.Phong, "ID", "TenPhong", albumAnh.Phong_ID);
            return View(albumAnh);
        }

        // GET: AlbumAnh/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumAnh albumAnh = db.AlbumAnh.Find(id);
            if (albumAnh == null)
            {
                return HttpNotFound();
            }
            ViewBag.Phong_ID = new SelectList(db.Phong, "ID", "TenPhong", albumAnh.Phong_ID);
            return View(albumAnh);
        }

        // POST: AlbumAnh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Phong_ID,AlbumHinhAnh")] AlbumAnh albumAnh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(albumAnh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Phong_ID = new SelectList(db.Phong, "ID", "TenPhong", albumAnh.Phong_ID);
            return View(albumAnh);
        }

        // GET: AlbumAnh/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumAnh albumAnh = db.AlbumAnh.Find(id);
            if (albumAnh == null)
            {
                return HttpNotFound();
            }
            return View(albumAnh);
        }

        // POST: AlbumAnh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlbumAnh albumAnh = db.AlbumAnh.Find(id);
            db.AlbumAnh.Remove(albumAnh);
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
