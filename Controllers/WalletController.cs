using FBC.Models;
using FBC.Services;
using FBC.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FBC.Controllers
{
    public class WalletController : Controller
    {

        private readonly IVnPayService _vnPayService;
        private readonly Fbc1Context _context;

        public WalletController(IVnPayService vnPayService, Fbc1Context context)
        {
            _vnPayService = vnPayService;
            _context = context;
        }

        public IActionResult Index()
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == User.Identity.GetUserId());
            ViewData["userEmail"] = user.Email;
            var wallet = _context.Wallets.FirstOrDefault(w => w.Id == User.Identity.GetUserId());
            ViewData["userCredir"] = wallet.Credit;
            return View();


        }
    }
}
