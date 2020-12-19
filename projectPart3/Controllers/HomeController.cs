using projectPart3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace projectPart3.Controllers
{
    public class HomeController : Controller
    {
        private LTQLDBContext db = new LTQLDBContext();
        public ActionResult Index()
        {
            var sanphams = db.sanphams.Include(s => s.danhmucs).Include(s => s.gias).Include(s => s.nhacungcaps).ToList();
            ViewBag.DanhMuc = db.danhmucs.ToList();
            ViewBag.message = "Home";
            return View(sanphams);

        }

        /*public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.DanhMuc = db.danhmucs.ToList();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.DanhMuc = db.danhmucs.ToList();
            return View();
        }*/
        public ActionResult ShowProduct(int? id, string sortOrder, string searchString)
        {
            ViewBag.Message = "Product";
            ViewBag.DanhMuc = db.danhmucs.ToList();
            ViewBag.TakeProduct = db.sanphams.ToList();

            ViewBag.TenSortParm = String.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";
            if(id == null)
            {
                ViewBag.countPro = db.sanphams.Count();
                var models = db.sanphams.AsQueryable();
                if (!String.IsNullOrEmpty(searchString))
                {
                    models = models.Where(s => s.ten_sp.Contains(searchString)
                                           || s.danhmucs.ten_danh_muc.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "ten_desc":
                        models = models.OrderByDescending(s => s.ten_sp);
                        break;
                    case "price":
                        models = models.OrderBy(s => s.gias.gia_khuyen_mai);
                        break;
                    case "price_desc":
                        models = models.OrderByDescending(s => s.gias.gia_khuyen_mai);
                        break;
                    default:
                        models = models.OrderBy(s => s.ma_gia);
                        break;
                }
                return View(models.ToList());
            }
            else
            {
                ViewBag.countPro = db.sanphams.Where(s => s.danhmucs.ma_danh_muc == id).Count();
                var models = db.sanphams.Where(s => s.danhmucs.ma_danh_muc == id).AsQueryable();
                if (!String.IsNullOrEmpty(searchString))
                {
                    models = models.Where(s => s.ten_sp.Contains(searchString)
                                           || s.danhmucs.ten_danh_muc.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "ten_desc":
                        models = models.OrderByDescending(s => s.ten_sp);
                        break;
                    case "price":
                        models = models.OrderBy(s => s.gias.gia_khuyen_mai);
                        break;
                    case "price_desc":
                        models = models.OrderByDescending(s => s.gias.gia_khuyen_mai);
                        break;
                    default:
                        models = models.OrderBy(s => s.ma_gia);
                        break;
                }
                return View(models.ToList());
            }
            

        }
        public ActionResult Detail(long id)
        {
            ViewBag.Message = "Detail";
            ViewBag.sanphams = db.sanphams.Find(id);
            ViewBag.DanhMuc = db.danhmucs.ToList();
            return View();
        }
        public ActionResult Cart()
        {
            ViewBag.title = "Cart";
            ViewBag.Message = "Your contact page.";
            ViewBag.DanhMuc = db.danhmucs.ToList();
            return View();
        }
        public ActionResult Checkout()
        {
            ViewBag.Message = "Checkout";
            ViewBag.DanhMuc = db.danhmucs.ToList();
            return View();
        }
    }
}