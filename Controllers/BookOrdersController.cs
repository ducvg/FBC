using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FBC.Models;
using Microsoft.AspNet.Identity;

namespace FBC.Controllers
{
    public class BookOrdersController : Controller
    {
        private readonly Fbc1Context _context;

        public BookOrdersController(Fbc1Context context)
        {
            _context = context;
        }

        // GET: BookOrders
        public async Task<IActionResult> Index()
        {
            var fbc1Context = await _context.BookOrders.Include(b => b.User).Where(b => b.Id == User.Identity.GetUserId()).ToListAsync();
            return View(fbc1Context);
        }

        public ActionResult OrderReceiver(int? id)
        {
            var order = _context.BookOrders.Include(o => o.Books).Include(o => o.User).FirstOrDefault(o => o.BookOrderId == id);
            return View(order);
        }

        // GET: BookOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var bookOrder = await _context.BookOrders
            //    .Include(b => b.User)
            //    .FirstOrDefaultAsync(m => m.BookOrderId == id);
            var bookOrder = _context.BookOrders
                .Include(bo => bo.Books) 
                .FirstOrDefault(bo => bo.BookOrderId == id);

            if (bookOrder == null)
            {
                return NotFound();
            }
            
            return View(bookOrder);
        }

        // GET: BookOrders/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: BookOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookOrderId,Total,Status,Recipient,Address,OrderDate,ShippedDate,Phone,Id")] BookOrder bookOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", bookOrder.Id);
            return View(bookOrder);
        }

        // GET: BookOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookOrder = await _context.BookOrders.FindAsync(id);
            if (bookOrder == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", bookOrder.Id);
            return View(bookOrder);
        }

        // POST: BookOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookOrderId,Total,Status,Recipient,Address,OrderDate,ShippedDate,Phone,Id")] BookOrder bookOrder)
        {
            if (id != bookOrder.BookOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookOrderExists(bookOrder.BookOrderId))
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
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", bookOrder.Id);
            return View(bookOrder);
        }

        // GET: BookOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookOrder = await _context.BookOrders
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BookOrderId == id);
            if (bookOrder == null)
            {
                return NotFound();
            }

            return View(bookOrder);
        }

        // POST: BookOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookOrder = await _context.BookOrders.FindAsync(id);
            if (bookOrder != null)
            {
                _context.BookOrders.Remove(bookOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookOrderExists(int id)
        {
            return _context.BookOrders.Any(e => e.BookOrderId == id);
        }
    }
}
