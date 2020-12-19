using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Web.Mvc;
using projectPart3.Models;
using System.Text;
using PagedList;

namespace projectPart3.Controllers
{
    public class KHACHController : Controller
    {
        private LTQLDBContext db = new LTQLDBContext();
        // GET: KHACH
        
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var ma_hoa_du_lieu = GETMD5(password);
                var kiem_tra_tai_khoan = db.khachs.Where(s => s.Email.Equals(email) && s.PassWord.Equals(ma_hoa_du_lieu)).ToList();
                if (kiem_tra_tai_khoan.Count() > 0 )
                {
                    Session["idKhach"] = kiem_tra_tai_khoan.FirstOrDefault().id_Khach;
                    Session["client"] = kiem_tra_tai_khoan.FirstOrDefault().Email;
                    Session["firstName"] = kiem_tra_tai_khoan.FirstOrDefault().FirstName;
                    Session["lastName"] = kiem_tra_tai_khoan.FirstOrDefault().LastName;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.LoginError = "Đăng nhập không thành công";
                    return RedirectToAction("DangNhap");
                }
            }
            return View();
        }

        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(KHACH khach)
        {
            if (ModelState.IsValid)
            {
                var check = db.khachs.FirstOrDefault(m => m.Email == khach.Email);
                if (check == null)
                {
                    khach.PassWord = GETMD5(khach.PassWord);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.khachs.Add(khach);
                    db.SaveChanges();
                    return RedirectToAction("DangNhap");
                }
                else
                {
                    ViewBag.EmailError = "Email đã tồn tại";
                    return View();
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index","Home");
        }
        private string GetMD5(string passWord)
        {
            throw new NotImplementedException();
        }

        public static string GETMD5(string pass)
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