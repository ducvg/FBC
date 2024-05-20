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

        // GET: Cart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartOrder = await _context.CartOrders
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cartOrder == null)
            {
                return NotFound();
            }

            return View(cartOrder);
        }

        // GET: Cart/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Cart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,UserId")] CartOrder cartOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", cartOrder.UserId);
            return View(cartOrder);
        }

        // GET: Cart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartOrder = await _context.CartOrders.FindAsync(id);
            if (cartOrder == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", cartOrder.UserId);
            return View(cartOrder);
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,UserId")] CartOrder cartOrder)
        {
            if (id != cartOrder.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartOrderExists(cartOrder.CartId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", cartOrder.UserId);
            return View(cartOrder);
        }

        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartOrder = await _context.CartOrders
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cartOrder == null)
            {
                return NotFound();
            }

            return View(cartOrder);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartOrder = await _context.CartOrders.FindAsync(id);
            if (cartOrder != null)
            {
                _context.CartOrders.Remove(cartOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartOrderExists(int id)
        {
            return _context.CartOrders.Any(e => e.CartId == id);
        }
    }
}
