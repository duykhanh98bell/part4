using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using projectPart3.Models;

namespace projectPart3.Controllers
{
    public class ChiTietHoaDonsController : Controller
    {
        private LTQLDBContext db = new LTQLDBContext();

        // GET: ChiTietHoaDons
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                ViewBag.Hoadon = db.chitiethoadons.ToList();
            }
            ViewBag.Hoadon = db.chitiethoadons.Where(s => s.hoadons.ma_hd == id).ToList();
            return View();
        }

        // GET: ChiTietHoaDons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.chitiethoadons.Find(id);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHoaDon);
        }

        // GET: ChiTietHoaDons/Create
        public ActionResult Create()
        {
            ViewBag.ma_hd = new SelectList(db.hoadons, "ma_hd", "ghi_chu");
            ViewBag.ma_san_pham = new SelectList(db.sanphams, "ma_sp", "ten_sp");
            return View();
        }

        // POST: ChiTietHoaDons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ma_hd_chi_tiet,ma_hd,ma_san_pham,so_luong")] ChiTietHoaDon chiTietHoaDon)
        {
            if (ModelState.IsValid)
            {
                db.chitiethoadons.Add(chiTietHoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ma_hd = new SelectList(db.hoadons, "ma_hd", "ghi_chu", chiTietHoaDon.ma_hd);
            ViewBag.ma_san_pham = new SelectList(db.sanphams, "ma_sp", "ten_sp", chiTietHoaDon.ma_san_pham);
            return View(chiTietHoaDon);
        }

        // GET: ChiTietHoaDons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.chitiethoadons.Find(id);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            ViewBag.ma_hd = new SelectList(db.hoadons, "ma_hd", "ghi_chu", chiTietHoaDon.ma_hd);
            ViewBag.ma_san_pham = new SelectList(db.sanphams, "ma_sp", "ten_sp", chiTietHoaDon.ma_san_pham);
            return View(chiTietHoaDon);
        }

        // POST: ChiTietHoaDons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ma_hd_chi_tiet,ma_hd,ma_san_pham,so_luong")] ChiTietHoaDon chiTietHoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietHoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ma_hd = new SelectList(db.hoadons, "ma_hd", "ghi_chu", chiTietHoaDon.ma_hd);
            ViewBag.ma_san_pham = new SelectList(db.sanphams, "ma_sp", "ten_sp", chiTietHoaDon.ma_san_pham);
            return View(chiTietHoaDon);
        }

        // GET: ChiTietHoaDons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.chitiethoadons.Find(id);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHoaDon);
        }

        // POST: ChiTietHoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietHoaDon chiTietHoaDon = db.chitiethoadons.Find(id);
            db.chitiethoadons.Remove(chiTietHoaDon);
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
