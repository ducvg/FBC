using FBC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using System.Diagnostics;

namespace FBC.Controllers
{
    public class HomeController : Controller
    {
        private readonly Fbc1Context context;


        public HomeController(Fbc1Context context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            
            var books = await context.Books.OrderByDescending(c => c.BookId).Take(8).ToListAsync();
            if (User.Identity.IsAuthenticated)
            {
                var user = context.Users.FirstOrDefault(u => u.Id == User.Identity.GetUserId());
                var wallet = context.Wallets.FirstOrDefault(w => w.Id == user.Id);
                var cart = context.CartOrders.Include(c => c.Books).FirstOrDefault(c => c.Id == user.Id);
                if (cart == null)
                {
                    cart = new CartOrder
                    {
                        Id = user.Id
                    };
                    await context.CartOrders.AddAsync(cart);
                    await context.SaveChangesAsync();
                }

                if (wallet == null)
                {
                    wallet = new Wallet
                    {
                        Id = user.Id,
                    };
                    await context.Wallets.AddAsync(wallet);
                    await context.SaveChangesAsync();
                }
            }
            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
