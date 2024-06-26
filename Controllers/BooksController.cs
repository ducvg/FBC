﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FBC.Models;
using Microsoft.IdentityModel.Tokens;

namespace FBC.Controllers
{
    public class BooksController : Controller
    {
        private readonly Fbc1Context _context;

        public BooksController(Fbc1Context context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(int? cate, string? search)
        {
            var booklist = _context.Books.Include(b => b.Categories).ToList();
            if (cate.HasValue)
            {
                booklist = booklist.Where(b => b.Categories.Any(c => c.CategoryId == cate)).ToList();
                if(booklist == null)
                {
                    TempData["Message"] = $"Chưa có sản phẩm";
                }
            }
            if(!search.IsNullOrEmpty())
            {
                booklist = booklist.Where(b=>b.Title.ToLower().Contains(search.ToLower())).ToList();
                if (booklist == null)
                {
                    TempData["Message"] = $"Chưa có sản phẩm";
                }
            }
            if (!search.IsNullOrEmpty() && cate.HasValue)
            {
                booklist = booklist.Where(b => b.Title.ToLower().Contains(search.ToLower()) && b.Categories.Any(c => c.CategoryId == cate)).ToList();
                if (booklist == null)
                {
                    TempData["Message"] = $"Chưa có sản phẩm";
                }
            }
            if (booklist == null)
            {
                TempData["Message"] = $"Chưa có sản phẩm";
            }
            return View(booklist);
        }

        public async Task<IActionResult> testbook(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.Include(b => b.Categories)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book); 
        }

       
        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.Include(b => b.Categories).FirstOrDefaultAsync(b => b.BookId == id);
            if (book == null)
            {

                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Title,Author,Publisher,Description,Condition,NoPage,Weight,Width,Length,Height,Image1,Image2,Image3,Image4,Status,Credit")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,Author,Publisher,Description,Condition,NoPage,Weight,Width,Length,Height,Image1,Image2,Image3,Image4,Status,Credit")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
