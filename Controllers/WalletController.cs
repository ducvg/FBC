using FBC.Models;
using FBC.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace FBC.Controllers
{
    public class WalletController : Controller
    {

        private readonly Fbc1Context _context;

        public WalletController( Fbc1Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == User.Identity.GetUserId());
            ViewData["userEmail"] = user.Email;
            var wallet = _context.Wallets.FirstOrDefault(w => w.Id == User.Identity.GetUserId());
            ViewData["userCredit"] = wallet.Credit.Value.ToString("#,##0.");
            string prefix = "FBCPC";
            List<string> generatedCodes = new List<string>();
            while (true)
            {
                string randomDigits = GenerateRandomDigits(6);
                string code = prefix + randomDigits;
                if (!generatedCodes.Contains(code))
                {
                    generatedCodes.Add(code);
                    ViewBag.Code = code;
                    break;
                }
            }  
            return View();

        }
        private static string GenerateRandomDigits(int length)
        {
            string digits = "0123456789";
            StringBuilder randomDigits = new StringBuilder();

            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                randomDigits.Append(digits[random.Next(digits.Length)]);
            }

            return randomDigits.ToString();
        }

        public async Task<IActionResult> Pay(string bankname, string code, decimal Amount, string description)
        {
            if (string.IsNullOrEmpty(bankname) || string.IsNullOrEmpty(code) || Amount == null)
            {
                TempData["Message"] = "BankName,Code and Amount Field is required";
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == User.Identity.GetUserId());
            var walletorder = new WalletOrder
            {
                BankAcountName = bankname,
                PaymentCode = code,
                Amount = Amount,
                Credit = Math.Floor(Amount / 1000),
                Description = description,
                CreatedDate = DateTime.Now,
                Id = user.Id,
            };
            await _context.WalletOrders.AddAsync(walletorder);
            await _context.SaveChangesAsync();
            TempData["Message"] = $"Payment success ! Your request has been sent";
            return RedirectToAction("Index");

        }
    }
}
