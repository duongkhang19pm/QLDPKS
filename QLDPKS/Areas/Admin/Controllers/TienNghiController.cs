using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
    public class TienNghiController : Controller
    {
        private QLKSEntities db = new QLKSEntities();

        // GET: Admin/TienNghi
        public ActionResult Index()
        {
            return View(db.TienNghi.ToList());
        }

        // GET: Admin/TienNghi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TienNghi tienNghi = db.TienNghi.Find(id);
            if (tienNghi == null)
            {
                return HttpNotFound();
            }
            return View(tienNghi);
        }

        // GET: Admin/TienNghi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TienNghi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenTienNghi")] TienNghi tienNghi)
        {
            if (ModelState.IsValid)
            {
                db.TienNghi.Add(tienNghi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tienNghi);
        }

        // GET: Admin/TienNghi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TienNghi tienNghi = db.TienNghi.Find(id);
            if (tienNghi == null)
            {
                return HttpNotFound();
            }
            return View(tienNghi);
        }

        // POST: Admin/TienNghi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenTienNghi")] TienNghi tienNghi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tienNghi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tienNghi);
        }

        // GET: Admin/TienNghi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TienNghi tienNghi = db.TienNghi.Find(id);
            if (tienNghi == null)
            {
                return HttpNotFound();
            }
            return View(tienNghi);
        }

        // POST: Admin/TienNghi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TienNghi tienNghi = db.TienNghi.Find(id);
            db.TienNghi.Remove(tienNghi);
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
