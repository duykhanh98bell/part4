using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projectPart3.Models
{
    public class CartItem
    {
        public SanPham _shopping_product { get; set; }
        public int _shopping_quantity { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void add(SanPham _pro, int _quantity = 1)
        {
            var item = items.FirstOrDefault(s => s._shopping_product.ma_sp == _pro.ma_sp);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    _shopping_product = _pro,
                    _shopping_quantity = _quantity
                });

            }
            else
            {
                item._shopping_quantity += _quantity;
            }
        }
        public void update_quantity_shopping(int id, int _quantity = 1)
        {
            var item = items.Find(s => s._shopping_product.ma_sp == id);
            if(item != null)
            {
                item._shopping_quantity = _quantity;
            }
        }
        public void update_quantity_shopping_detail(SanPham _pro, int _quantity)
        {
            var item = items.FirstOrDefault(s => s._shopping_product.ma_sp == _pro.ma_sp);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    _shopping_product = _pro,
                    _shopping_quantity = _quantity
                });

            }
            else
            {
                item._shopping_quantity += _quantity;
            }
        }
        public double total_money()
        {
            var total = items.Sum(s => s._shopping_product.gias.gia_khuyen_mai * s._shopping_quantity);
            return (double)total;
        }
        public void removeCartItem(int id)
        {
            items.RemoveAll(s => s._shopping_product.ma_sp == id);
        }
        public int totalQuantityCart()
        {
            return items.Sum(s => s._shopping_quantity);
        }
        public void clearCart()
        {
            items.Clear();
        }
    }
}