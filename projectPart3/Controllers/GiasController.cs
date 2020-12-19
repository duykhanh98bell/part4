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
    public class GiasController : Controller
    {
        private LTQLDBContext db = new LTQLDBContext();

        // GET: Gias
        public ActionResult Index()
        {
            return View(db.gias.ToList());
        }

        // GET: Gias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gia gia = db.gias.Find(id);
            if (gia == null)
            {
                return HttpNotFound();
            }
            return View(gia);
        }

        // GET: Gias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ma_gia,gia_goc,gia_khuyen_mai,ngay_ap_dung")] Gia gia)
        {
            if (ModelState.IsValid)
            {
                db.gias.Add(new Gia
                {
                    gia_goc = gia.gia_goc,
                    gia_khuyen_mai = gia.gia_khuyen_mai,
                    ngay_ap_dung = DateTime.Now
                });
                db.SaveChanges();
                return RedirectToAction("Index");   
            }

            return View(gia);
        }

        // GET: Gias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gia gia = db.gias.Find(id);
            if (gia == null)
            {
                return HttpNotFound();
            }
            return View(gia);
        }

        // POST: Gias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ma_gia,gia_goc,gia_khuyen_mai,ngay_ap_dung")] Gia gia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gia);
        }

        // GET: Gias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gia gia = db.gias.Find(id);
            if (gia == null)
            {
                return HttpNotFound();
            }
            return View(gia);
        }

        // POST: Gias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gia gia = db.gias.Find(id);
            db.gias.Remove(gia);
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
