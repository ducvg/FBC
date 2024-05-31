using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FBC.Models;
using FBC.Helpers;
using FBC.ViewModels;
using Microsoft.AspNet.Identity;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.AspNetCore.Identity;
using NuGet.Packaging;

namespace FBC.Controllers
{
    public class CartController : Controller
    {
        private readonly Fbc1Context _context;

        public CartController(Fbc1Context context)
        {
            _context = context;
        }


        // GET: Cart
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == User.Identity.GetUserId());
            var cart = _context.CartOrders.Include(c => c.Books).FirstOrDefault(c => c.Id == user.Id);

            if (cart == null)
            {
                return View(new CartOrder()); 
            }
            decimal total = (decimal)cart.Books.Sum(b => b.Credit );
            ViewData["CartTotal"] = total.ToString("#,##0.");

            var cartViewModel = new CartOrder
            {
                CartId = cart.CartId,
                Books = cart.Books.Select(b => new Book
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Image1 = b.Image1 ?? string.Empty,
                    Credit = b.Credit ?? 0,
                    Condition = b.Condition,
                }).ToList()
            };
            ViewData["Total"] = (total + 10).ToString("#,##0.");
            return View(cart);
        }


        public async Task<IActionResult> AddToCart(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == User.Identity.GetUserId());
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var cart = await _context.CartOrders.Include(c => c.Books).FirstOrDefaultAsync(c => c.Id == user.Id);
            //if (cart == null)
            //{
            //    cart = new CartOrder { Id = user.Id };
            //    _context.CartOrders.Add(cart);
            //}

            var existingBook = cart.Books.FirstOrDefault(b => b.BookId == id);
            if (existingBook != null)
            {
                return RedirectToAction("Index");
            }
            cart.Books.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveCart(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account"); 
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == User.Identity.GetUserId());
            var cart = _context.CartOrders.Include(c => c.Books).FirstOrDefault(c => c.Id == user.Id);

            if (cart == null)
            {
                return RedirectToAction("Index"); 
            }

            var bookToRemove = cart.Books.FirstOrDefault(b => b.BookId == id);

            if (bookToRemove == null)
            {
                return RedirectToAction("Index"); 
            }
            cart.Books.Remove(bookToRemove);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Checkout(BookOrder order)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == User.Identity.GetUserId());
            var wallet = _context.Wallets.FirstOrDefault(w => w.Id == user.Id);


            if (!DeductCredit(order.Total.Value))
            {
                TempData["SuccessMessage"] = "Thanh toán không thành công, bạn không đủ điểm thanh toán!";
                return RedirectToAction("Index");
            }
            order.Status = 1;
            order.Recipient = order.Recipient;
            order.Address = order.Address;
            order.Phone = order.Phone;
            order.Total = order.Total;
            order.OrderDate = DateTime.Now;
            order.Id = user.Id;
            _context.BookOrders.Add(order);
            _context.SaveChanges();

            var savedOrder = _context.BookOrders.Include(bo => bo.Books).FirstOrDefault(bo => bo.BookOrderId == order.BookOrderId);

            var cart = _context.CartOrders.Include(c => c.Books).FirstOrDefault(c => c.Id == user.Id);
            TempData["orderItem"] = cart.Books.ToList().ToString();
            if (cart != null)
            {
                order.Books.AddRange(cart.Books);
                cart.Books.Clear();
                _context.SaveChanges();
            }

            TempData["SuccessMessage"] = "Thanh toán thành công!";
            return RedirectToAction("Index", "BookOrders", new { id = savedOrder.BookOrderId } );
        }

        public bool DeductCredit(decimal amount)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == User.Identity.GetUserId());
            var wallet = _context.Wallets.Include(w => w.User).FirstOrDefault(w => w.Id == user.Id);
            if (wallet.Credit >= amount)
            {
                wallet.Credit -= amount;
                return true;
            }

            return false;
        }
    }
}
