using FBC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FBC.Controllers
{
    public class BookOrdersController : Controller
    {
        private readonly Fbc1Context _context;

        public BookOrdersController(Fbc1Context context)
        {
            _context = context;
        }



        // GET: BookOrdersController
        public ActionResult Index(int? id)
        {
            var order = _context.BookOrders.Include(o => o.User).FirstOrDefault(o => o.BookOrderId == id);
            return View(order);
        }


        // GET: BookOrdersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookOrdersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookOrdersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookOrdersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookOrdersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookOrdersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookOrdersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
