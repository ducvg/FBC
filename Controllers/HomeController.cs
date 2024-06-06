using FBC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
