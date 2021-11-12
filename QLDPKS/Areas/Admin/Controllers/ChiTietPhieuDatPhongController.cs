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
    public class ChiTietPhieuDatPhongController : XacThucController
    {
        private QLKSEntities db = new QLKSEntities();

        // GET: Admin/ChiTietPhieuDatPhong
        public ActionResult Index()
        {
            var chiTiets = db.ChiTietPhieuDatPhong.Include(c => c.Phong).Include(c => c.PhieuDatPhong);
            return View(chiTiets.ToList());
        }



        // GET: Admin/PhieuDatPhong/Details/5
        public ActionResult HoaDon(int id)
        {

            var phieu = db.ChiTietPhieuDatPhong.Where(p => p.ID == id).SingleOrDefault();
            return View(phieu);
        }
        // GET: Home/ThanhToan
        public ActionResult Create()
        {

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChiTietPhieuDatPhong chiTietPhieuDat, int id)
        {
            if (ModelState.IsValid)
            {
                var phieu = db.PhieuDatPhong.Find(id);
                List<DatPhongKS> cart = (List<DatPhongKS>)Session["cart"];
                foreach (var item in cart)
                {
                    chiTietPhieuDat.PhieuDatPhong_ID = phieu.ID;
                    chiTietPhieuDat.Phong_ID = item.phong.ID;
                    chiTietPhieuDat.SoLuong = Convert.ToInt16(item.soNgay);
                    chiTietPhieuDat.DonGia = (chiTietPhieuDat.SoLuong * item.phong.Gia);
                    db.ChiTietPhieuDatPhong.Add(chiTietPhieuDat);
                    db.SaveChanges();
                }
                cart.Clear();
                SetAlert("Thanh Toán  Thành Công", "success");
                // Quay về trang chủ
                return RedirectToAction("Index", "Home");
            }
            return View(chiTietPhieuDat);
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
