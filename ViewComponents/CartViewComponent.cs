using FBC.Helpers;
using FBC.Models;
using FBC.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FBC.ViewComponents
{
    public class CartViewComponent: ViewComponent
    {
        private readonly Fbc1Context _context;

        public CartViewComponent(Fbc1Context context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == User.Identity.GetUserId());
            var wallet = _context.Wallets.Include(w => w.User).FirstOrDefault(w => w.Id == user.Id);
            var cart = _context.CartOrders.Include(c => c.Books).FirstOrDefault(c => c.Id == user.Id);
            ViewData["Credit"] = wallet.Credit.Value.ToString("#,##0.");
            return View("CartPanel", new CartModel
            {
                Quantity = cart.Books.Count,

            });
        }
    }
}
