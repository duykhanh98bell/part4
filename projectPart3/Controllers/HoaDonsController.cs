using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using PagedList.Mvc;
using PagedList;
using System.Web.Mvc;
using projectPart3.Models;

namespace projectPart3.Controllers
{
    public class HoaDonsController : Controller
    {
        private LTQLDBContext db = new LTQLDBContext();

        // GET: HoaDons
        public ActionResult Index()
        {
            var hoadons = db.hoadons.Include(h => h.khachs);
            return View(hoadons.ToList());
        }
        [HttpGet]
        public ActionResult Index(int? size, int? page)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "5", Value = "5" });
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "20", Value = "20" });
            items.Add(new SelectListItem { Text = "25", Value = "25" });
            items.Add(new SelectListItem { Text = "35", Value = "35" });

            foreach(var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }
            ViewBag.size = items;
            ViewBag.currentSize = size;

            page = page ?? 1;
            var links = (from l in db.hoadons
                         select l).OrderBy(x => x.ma_hd);
            int pageSize = (size ?? 5);

            int pageNumber = (page ?? 1);
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        // GET: HoaDons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.hoadons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // GET: HoaDons/Create
        public ActionResult Create()
        {
            ViewBag.ma_kh = new SelectList(db.khachs, "id_Khach", "FirstName");
            return View();
        }

        // POST: HoaDons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ma_hd,ma_kh,tong_tien,trang_thai,ghi_chu,ngay_tao")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.hoadons.Add(hoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ma_kh = new SelectList(db.khachs, "id_Khach", "FirstName", hoaDon.ma_kh);
            return View(hoaDon);
        }

        // GET: HoaDons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.hoadons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            ViewBag.ma_kh = new SelectList(db.khachs, "id_Khach", "FirstName", hoaDon.ma_kh);
            return View(hoaDon);
        }

        // POST: HoaDons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ma_hd,ma_kh,tong_tien,trang_thai,ghi_chu,ngay_tao")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ma_kh = new SelectList(db.khachs, "id_Khach", "FirstName", hoaDon.ma_kh);
            return View(hoaDon);
        }

        // GET: HoaDons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.hoadons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: HoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoaDon hoaDon = db.hoadons.Find(id);
            db.hoadons.Remove(hoaDon);
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
