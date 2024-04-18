using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DWP2.Data;
using DWP2.Models;

namespace DWP2.Controllers
{
    public class Product_CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Product_CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product_Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product_Categories.ToListAsync());
        }

        // GET: Product_Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product_Categories = await _context.Product_Categories
                .FirstOrDefaultAsync(m => m.CATEGORY_ID == id);
            if (product_Categories == null)
            {
                return NotFound();
            }

            return View(product_Categories);
        }

        // GET: Product_Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product_Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CATEGORY_ID,CATEGORY_NAME")] Product_Categories product_Categories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product_Categories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product_Categories);
        }

        // GET: Product_Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product_Categories = await _context.Product_Categories.FindAsync(id);
            if (product_Categories == null)
            {
                return NotFound();
            }
            return View(product_Categories);
        }

        // POST: Product_Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CATEGORY_ID,CATEGORY_NAME")] Product_Categories product_Categories)
        {
            if (id != product_Categories.CATEGORY_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product_Categories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Product_CategoriesExists(product_Categories.CATEGORY_ID))
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
            return View(product_Categories);
        }

        // GET: Product_Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product_Categories = await _context.Product_Categories
                .FirstOrDefaultAsync(m => m.CATEGORY_ID == id);
            if (product_Categories == null)
            {
                return NotFound();
            }

            return View(product_Categories);
        }

        // POST: Product_Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product_Categories = await _context.Product_Categories.FindAsync(id);
            if (product_Categories != null)
            {
                _context.Product_Categories.Remove(product_Categories);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Product_CategoriesExists(int id)
        {
            return _context.Product_Categories.Any(e => e.CATEGORY_ID == id);
        }
    }
}
