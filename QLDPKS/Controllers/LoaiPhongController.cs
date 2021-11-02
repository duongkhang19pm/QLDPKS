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
    public class LoaiPhongController : Controller
    {
        private QLKSEntities db = new QLKSEntities();

        // GET: Admin/LoaiPhong
        public ActionResult Index()
        {
            return View(db.LoaiPhong.ToList());
        }

        // GET: Admin/LoaiPhong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiPhong loaiPhong = db.LoaiPhong.Find(id);
            if (loaiPhong == null)
            {
                return HttpNotFound();
            }
            return View(loaiPhong);
        }

        // GET: Admin/LoaiPhong/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiPhong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenLoai")] LoaiPhong loaiPhong)
        {
            if (ModelState.IsValid)
            {
                db.LoaiPhong.Add(loaiPhong);
                db.SaveChanges();
                SetAlert("Thêm Loại Thành Công", "success");
                return RedirectToAction("Index");
            }

            return View(loaiPhong);
        }

        // GET: Admin/LoaiPhong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiPhong loaiPhong = db.LoaiPhong.Find(id);
            if (loaiPhong == null)
            {
                return HttpNotFound();
            }
            return View(loaiPhong);
        }

        // POST: Admin/LoaiPhong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenLoai")] LoaiPhong loaiPhong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiPhong).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập Nhật Thành Công", "success");
                return RedirectToAction("Index");
            }
            return View(loaiPhong);
        }

        // GET: Admin/LoaiPhong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiPhong loaiPhong = db.LoaiPhong.Find(id);
            if (loaiPhong == null)
            {
                return HttpNotFound();
            }
            return View(loaiPhong);
        }

        // POST: Admin/LoaiPhong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiPhong loaiPhong = db.LoaiPhong.Find(id);
            db.LoaiPhong.Remove(loaiPhong);
            db.SaveChanges();
            SetAlert("Xóa Thành Công", "success");
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
