﻿using System;
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
    public class PhieuDatPhongController : Controller
    {
        private QLKSEntities db = new QLKSEntities();

        // GET: Admin/PhieuDatPhong
        public ActionResult Index()
        {
            var phieuDatPhong = db.PhieuDatPhong.Include(p => p.KhachHang).Include(p => p.NhanVien);
            return View(phieuDatPhong.ToList());
        }

        // GET: Admin/PhieuDatPhong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDatPhong phieuDatPhong = db.PhieuDatPhong.Find(id);
            if (phieuDatPhong == null)
            {
                return HttpNotFound();
            }
            return View(phieuDatPhong);
        }

        // GET: Admin/PhieuDatPhong/Create
        public ActionResult Create()
        {
            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaTen");
            ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HoVaTenNV");
            return View();
        }

        // POST: Admin/PhieuDatPhong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,KhachHang_ID,NhanVien_ID,SDTLienHe,NgayDen,NgayDi,HinhThucThanhToan,TinhTrang,NgayDatPhong")] PhieuDatPhong phieuDatPhong)
        {
            if (ModelState.IsValid)
            {
                db.PhieuDatPhong.Add(phieuDatPhong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaTen", phieuDatPhong.KhachHang_ID);
            ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HoVaTenNV", phieuDatPhong.NhanVien_ID);
            return View(phieuDatPhong);
        }

        // GET: Admin/PhieuDatPhong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDatPhong phieuDatPhong = db.PhieuDatPhong.Find(id);
            if (phieuDatPhong == null)
            {
                return HttpNotFound();
            }
            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaTen", phieuDatPhong.KhachHang_ID);
            ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HoVaTenNV", phieuDatPhong.NhanVien_ID);
            return View(phieuDatPhong);
        }

        // POST: Admin/PhieuDatPhong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,KhachHang_ID,NhanVien_ID,SDTLienHe,NgayDen,NgayDi,HinhThucThanhToan,TinhTrang,NgayDatPhong")] PhieuDatPhong phieuDatPhong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuDatPhong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KhachHang_ID = new SelectList(db.KhachHang, "ID", "HoVaTen", phieuDatPhong.KhachHang_ID);
            ViewBag.NhanVien_ID = new SelectList(db.NhanVien, "ID", "HoVaTenNV", phieuDatPhong.NhanVien_ID);
            return View(phieuDatPhong);
        }

        // GET: Admin/PhieuDatPhong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDatPhong phieuDatPhong = db.PhieuDatPhong.Find(id);
            if (phieuDatPhong == null)
            {
                return HttpNotFound();
            }
            return View(phieuDatPhong);
        }

        // POST: Admin/PhieuDatPhong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuDatPhong phieuDatPhong = db.PhieuDatPhong.Find(id);
            db.PhieuDatPhong.Remove(phieuDatPhong);
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