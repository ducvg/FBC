using FBC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FBC.Controllers
{
    public class AdminController : Controller
    {
        private readonly Fbc1Context _context;

        public AdminController(Fbc1Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> ExchangeRequest()
        {
            List<ExchangeRequest> request = await _context.ExchangeRequests.Include(r => r.User).ToListAsync();
            return View(request);
        }
    }
}
