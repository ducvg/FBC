using FBC.Models;
using Microsoft.AspNetCore.Mvc;

namespace FBC.Controllers
{
    public class AdminController : Controller
    {
        private readonly Fbc1Context _context;

        public AdminController(Fbc1Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
