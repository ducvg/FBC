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
    public class ExchangeController : Controller
    {
        private readonly Fbc1Context _context;

        public ExchangeController(Fbc1Context context)
        {
            _context = context;
        }

        // GET: Exchange
        public async Task<IActionResult> Index()
        {            
            return View();
        }

        // GET: Exchange/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exchangeRequest = await _context.ExchangeRequests
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.ExchangeId == id);
            if (exchangeRequest == null)
            {
                return NotFound();
            }

            return View(exchangeRequest);
        }

        // GET: Exchange/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Exchange/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExchangeId,Title,Category,Author,Description,Condition,Image1,Image2,Image3,Image4,Status,Credit,Id")] ExchangeRequest exchangeRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exchangeRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", exchangeRequest.Id);
            return View(exchangeRequest);
        }

        // GET: Exchange/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exchangeRequest = await _context.ExchangeRequests.FindAsync(id);
            if (exchangeRequest == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", exchangeRequest.Id);
            return View(exchangeRequest);
        }

        // POST: Exchange/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExchangeId,Title,Category,Author,Description,Condition,Image1,Image2,Image3,Image4,Status,Credit,Id")] ExchangeRequest exchangeRequest)
        {
            if (id != exchangeRequest.ExchangeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exchangeRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExchangeRequestExists(exchangeRequest.ExchangeId))
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
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", exchangeRequest.Id);
            return View(exchangeRequest);
        }

        // GET: Exchange/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exchangeRequest = await _context.ExchangeRequests
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.ExchangeId == id);
            if (exchangeRequest == null)
            {
                return NotFound();
            }

            return View(exchangeRequest);
        }

        // POST: Exchange/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exchangeRequest = await _context.ExchangeRequests.FindAsync(id);
            if (exchangeRequest != null)
            {
                _context.ExchangeRequests.Remove(exchangeRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExchangeRequestExists(int id)
        {
            return _context.ExchangeRequests.Any(e => e.ExchangeId == id);
        }
    }
}
