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
    public class InventoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Inventories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Inventories.Include(i => i.Products).Include(i => i.Warehouses);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventories = await _context.Inventories
                .Include(i => i.Products)
                .Include(i => i.Warehouses)
                .FirstOrDefaultAsync(m => m.PRODUCT_ID == id);
            if (inventories == null)
            {
                return NotFound();
            }

            return View(inventories);
        }

        // GET: Inventories/Create
        public IActionResult Create()
        {
            ViewData["PRODUCT_ID"] = new SelectList(_context.Products, "PRODUCT_ID", "PRODUCT_NAME");
            ViewData["WAREHOUSE_ID"] = new SelectList(_context.Warehouses, "WAREHOUSE_ID", "WAREHOUSE_NAME");
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PRODUCT_ID,WAREHOUSE_ID,QUANTITY")] Inventories inventories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PRODUCT_ID"] = new SelectList(_context.Products, "PRODUCT_ID", "PRODUCT_NAME", inventories.PRODUCT_ID);
            ViewData["WAREHOUSE_ID"] = new SelectList(_context.Warehouses, "WAREHOUSE_ID", "WAREHOUSE_NAME", inventories.WAREHOUSE_ID);
            return View(inventories);
        }

        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventories = await _context.Inventories.FindAsync(id);
            if (inventories == null)
            {
                return NotFound();
            }
            ViewData["PRODUCT_ID"] = new SelectList(_context.Products, "PRODUCT_ID", "PRODUCT_NAME", inventories.PRODUCT_ID);
            ViewData["WAREHOUSE_ID"] = new SelectList(_context.Warehouses, "WAREHOUSE_ID", "WAREHOUSE_NAME", inventories.WAREHOUSE_ID);
            return View(inventories);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PRODUCT_ID,WAREHOUSE_ID,QUANTITY")] Inventories inventories)
        {
            if (id != inventories.PRODUCT_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoriesExists(inventories.PRODUCT_ID))
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
            ViewData["PRODUCT_ID"] = new SelectList(_context.Products, "PRODUCT_ID", "PRODUCT_NAME", inventories.PRODUCT_ID);
            ViewData["WAREHOUSE_ID"] = new SelectList(_context.Warehouses, "WAREHOUSE_ID", "WAREHOUSE_NAME", inventories.WAREHOUSE_ID);
            return View(inventories);
        }

        // GET: Inventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventories = await _context.Inventories
                .Include(i => i.Products)
                .Include(i => i.Warehouses)
                .FirstOrDefaultAsync(m => m.PRODUCT_ID == id);
            if (inventories == null)
            {
                return NotFound();
            }

            return View(inventories);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventories = await _context.Inventories.FindAsync(id);
            if (inventories != null)
            {
                _context.Inventories.Remove(inventories);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoriesExists(int id)
        {
            return _context.Inventories.Any(e => e.PRODUCT_ID == id);
        }
    }
}
