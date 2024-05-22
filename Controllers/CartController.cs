using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FBC.Models;

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
            var fbc1Context = _context.CartOrders.Include(c => c.User);
            return View(await fbc1Context.ToListAsync());
        }
    }
}
