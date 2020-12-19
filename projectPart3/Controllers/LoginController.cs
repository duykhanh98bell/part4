using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using projectPart3.Models;

namespace projectPart3.Controllers
{
    public class LoginController : Controller
    {
        LTQLDBContext db = new LTQLDBContext();
        
        private static object md5;

        // GET: 
        public ActionResult list()
        {
            ViewBag.listKhach = db.khachhangs.ToList();
            return View(db.khachhangs.ToList());
        }



        // GET: DELETE TO LIST
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang kh = db.khachhangs.Find(id);
            if (kh == null)
            {
                return HttpNotFound();
            }
            return View("Delete");
        }



        // POST: DELETE TO LIST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachHang kh = db.khachhangs.Find(id);
            db.khachhangs.Remove(kh);
            db.SaveChanges();
            return RedirectToAction("list");
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email,string password)
        {
            if (ModelState.IsValid)
            {
                var ma_hoa_du_lieu = GETMD5(password);
                var kiem_tra_tai_khoan = db.khachhangs.Where(s => s.Email.Equals(email) && s.PassWord.Equals(ma_hoa_du_lieu)).ToList();
                if(kiem_tra_tai_khoan.Count() > 0)
                {
                    Session["idKhachHang"] = kiem_tra_tai_khoan.FirstOrDefault().ma_khach_hang;
                    Session["tenKH"] = kiem_tra_tai_khoan.FirstOrDefault().UserName;
                    return RedirectToAction("Index", "SanPhams");
                }
                else
                {
                    ViewBag.LoginError = "Đăng nhập không thành công";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                var checkEmail = db.khachhangs.FirstOrDefault(m => m.Email == kh.Email);
                if(checkEmail == null)
                {
                    kh.PassWord = GETMD5(kh.PassWord);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.khachhangs.Add(kh);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                } 
                else
                {
                    ViewBag.EmailError = "Email đã tồn tại";
                    return View();
                }
            }
            return View();
        }
        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }

        public static string GETMD5 (string pass)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(pass);
            byte[] targetData = md5.ComputeHash(fromData);
            string mk_da_ma_hoa = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                mk_da_ma_hoa += targetData[i].ToString("x2");

            }
            return mk_da_ma_hoa;

        }
    }
}