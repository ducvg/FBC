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
    public class WalletOrdersController : Controller
    {
        private readonly Fbc1Context _context;

        public WalletOrdersController(Fbc1Context context)
        {
            _context = context;
        }

        // GET: WalletOrders
        public async Task<IActionResult> Index()
        {
            var fbc1Context = _context.WalletOrders.Include(w => w.User);
            return View(await fbc1Context.ToListAsync());
        }

        // GET: WalletOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walletOrder = await _context.WalletOrders
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.WalletOrderId == id);
            if (walletOrder == null)
            {
                return NotFound();
            }

            return View(walletOrder);
        }

        // GET: WalletOrders/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: WalletOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WalletOrderId,BankAcountName,PaymentCode,Description,Amount,Credit,CreatedDate,Id")] WalletOrder walletOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(walletOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", walletOrder.Id);
            return View(walletOrder);
        }

        // GET: WalletOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walletOrder = await _context.WalletOrders.FindAsync(id);
            if (walletOrder == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", walletOrder.Id);
            return View(walletOrder);
        }

        // POST: WalletOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WalletOrderId,BankAcountName,PaymentCode,Description,Amount,Credit,CreatedDate,Id")] WalletOrder walletOrder)
        {
            if (id != walletOrder.WalletOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(walletOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WalletOrderExists(walletOrder.WalletOrderId))
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
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", walletOrder.Id);
            return View(walletOrder);
        }

        // GET: WalletOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walletOrder = await _context.WalletOrders
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.WalletOrderId == id);
            if (walletOrder == null)
            {
                return NotFound();
            }

            return View(walletOrder);
        }

        // POST: WalletOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var walletOrder = await _context.WalletOrders.FindAsync(id);
            if (walletOrder != null)
            {
                _context.WalletOrders.Remove(walletOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WalletOrderExists(int id)
        {
            return _context.WalletOrders.Any(e => e.WalletOrderId == id);
        }
    }
}
