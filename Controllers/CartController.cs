using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FBC.Models;
using FBC.Helpers;

namespace FBC.Controllers
{
    public class CartController : Controller
    {
        private readonly Fbc1Context _context;

        public CartController(Fbc1Context context)
        {
            _context = context;
        }

        const string CART_KEY = "MYCART";
        public List<CartOrder> Cart => HttpContext.Session.Get<List<CartOrder>>(CART_KEY) ?? new List<CartOrder>();

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            
            return View(Cart);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var cart = Cart;
            var item = cart.FirstOrDefault(c => c.CartId == id);
            if(item == null)
            {
                var books = _context.Books.FirstOrDefault(b => b.BookId == id);
                if(books == null)
                {
                    TempData["Message"] = $"No Book Found with id {id}";
                    return View("/404");  
                }
                else
                {
                    item = new CartOrder
                    {
                        BookId = books.BookId,
                        Title = books.Title,
                        Image1 = books.Image1 ?? string.Empty,
                        Credit = books.Credit ?? 0,
                        Condition = books.Condition,

                    };
                    cart.Add(item);
                }
            }
            else
            {
                TempData["Message"] = $"Đã thêm vào giỏ hàng";
                return RedirectToAction("Index");
            }
            HttpContext.Session.Set(CART_KEY, cart);
            return RedirectToAction("Index");
        }
    }
}
