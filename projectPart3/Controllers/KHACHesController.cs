using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using projectPart3.Models;

namespace projectPart3.Controllers
{
    public class KHACHesController : Controller
    {
        private LTQLDBContext db = new LTQLDBContext();

        // GET: KHACHes
        public ActionResult Index(string currenFilter, string searchString, int? page)
        {
            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currenFilter;
            }
            ViewBag.currenFilter = searchString;
            var visas = db.khachs.ToList();
            if(!String.IsNullOrEmpty(searchString))
            {
                visas = (List<KHACH>)visas.Where(s => s.UserName.Contains(searchString)
                || s.FirstName.Contains(searchString)
                || s.LastName.Contains(searchString)
                || s.Email.Contains(searchString));
            }
            visas.OrderByDescending(v => v.id_Khach);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(visas.ToPagedList(pageNumber, pageSize));
            
        }

        // GET: KHACHes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACH kHACH = db.khachs.Find(id);
            if (kHACH == null)
            {
                return HttpNotFound();
            }
            return View(kHACH);
        }

        // GET: KHACHes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KHACHes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Khach,FirstName,LastName,Email,UserName,PassWord,ComfirmPassWord")] KHACH kHACH)
        {
            if (ModelState.IsValid)
            {
                db.khachs.Add(kHACH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kHACH);
        }

        // GET: KHACHes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACH kHACH = db.khachs.Find(id);
            if (kHACH == null)
            {
                return HttpNotFound();
            }
            return View(kHACH);
        }

        // POST: KHACHes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Khach,FirstName,LastName,Email,UserName,PassWord,ComfirmPassWord")] KHACH kHACH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHACH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kHACH);
        }

        // GET: KHACHes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACH kHACH = db.khachs.Find(id);
            if (kHACH == null)
            {
                return HttpNotFound();
            }
            return View(kHACH);
        }

        // POST: KHACHes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KHACH kHACH = db.khachs.Find(id);
            db.khachs.Remove(kHACH);
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
