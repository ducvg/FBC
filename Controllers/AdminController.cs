using FBC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace FBC.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly Fbc1Context _context;

        public AdminController(Fbc1Context context)
        {
            _context = context;
        }

        [Route("ExchangeRequest/{pageNumber:int?}")]
        public async Task<IActionResult> ExchangeRequest(int? pageNumber)
        {
            int pageSize = 10;
            int count = await _context.ExchangeRequests.CountAsync();
            ViewData["totalPage"] = (int)Math.Ceiling(count / (double)pageSize);
            int currentPageNumber = pageNumber ?? 1;

            List<ExchangeRequest> request = await _context.ExchangeRequests
                .Include(r => r.User)
                .Skip((currentPageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return View(request);
        }

    }
}
