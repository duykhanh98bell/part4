using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projectPart3.Models;
using System.Data.Entity;

namespace projectPart3.Controllers
{
    public class CartController : Controller
    {
        LTQLDBContext db = new LTQLDBContext();
        // GET: Cart
        public Cart getCart()
        {
            
            Cart cart = Session["Cart"] as Cart;
            if(cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult addToCart( int id)
        {
            ViewBag.DanhMuc = db.danhmucs.ToList();
            var pro = db.sanphams.SingleOrDefault(s => s.ma_sp == id);
            if(pro != null)
            {
                getCart().add(pro);
            }
            return RedirectToAction("showToCart", "Cart");
        }
        public ActionResult showToCart()
        {
            ViewBag.Message = "Cart";
            ViewBag.DanhMuc = db.danhmucs.ToList();
            if (Session["Cart"] == null)
            {
                return RedirectToAction("showToCart", "Cart");
            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult updateDetailCart(FormCollection form)
        {
            int id_pro = int.Parse(form["id_product"]);
            int quantity = int.Parse(form["quantity"]);
            var pro = db.sanphams.SingleOrDefault(s => s.ma_sp == id_pro);
            if (pro != null)
            {
                getCart().update_quantity_shopping_detail(pro, quantity);
            }
            return RedirectToAction("showToCart", "Cart");
        }
        public ActionResult updateCart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro =int.Parse(form["id_product"]);
            int quantity = int.Parse(form["quantity"]);
            cart.update_quantity_shopping(id_pro, quantity);
            return RedirectToAction("showToCart", "Cart");
        }
        public ActionResult removeCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.removeCartItem(id);
            return RedirectToAction("showToCart", "Cart");
        }
        public ActionResult Shopping_Success()
        {
            return View();
        }
        public PartialViewResult bagCart()
        {
            int total_item = 0;
            
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
                total_item = cart.totalQuantityCart();
                ViewBag.QuantityCart = total_item;
                return PartialView("bagCart");
        }
        public PartialViewResult total_money()
        {
            double money = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
                money = cart.total_money();
            ViewBag.Total = money;
            return PartialView("total_money");
        }

        public ActionResult shoping_success()
        {
            ViewBag.DanhMuc = db.danhmucs.ToList();
            return View();
        }
        public ActionResult checkOut(FormCollection form)
        {
            ViewBag.DanhMuc = db.danhmucs.ToList();
            
                Cart cart = Session["Cart"] as Cart;
                HoaDon _order = new HoaDon();
                _order.ngay_tao = DateTime.Now;
                _order.ma_kh = int.Parse(form["ma_kh"]);
                _order.ghi_chu = form["ghi_chu"];
                _order.trang_thai = int.Parse(form["trang_thai"]);
                _order.tong_tien = cart.Items.Sum(s => s._shopping_product.gias.gia_khuyen_mai * s._shopping_quantity);
                db.hoadons.Add(_order);
                foreach(var item in cart.Items)
                {
                    ChiTietHoaDon _order_detail = new ChiTietHoaDon();
                    _order_detail.ma_hd = _order.ma_hd;
                    _order_detail.ma_san_pham = item._shopping_product.ma_sp;
                    _order_detail.gia_goc = item._shopping_product.gias.gia_goc;
                    _order_detail.gia_khuyen_mai = item._shopping_product.gias.gia_khuyen_mai;
                     _order_detail.so_luong = item._shopping_quantity;
                    db.chitiethoadons.Add(_order_detail);
                }
                db.SaveChanges();
                cart.clearCart();
                return RedirectToAction("shoping_success", "Cart");
           
        }
        public ActionResult ShowCheckout()
        {
            ViewBag.Message = "Checkout";
            ViewBag.DanhMuc = db.danhmucs.ToList();
            if (Session["Cart"] == null)
            {
                return RedirectToAction("showToCart", "Cart");
            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
     
    }
}