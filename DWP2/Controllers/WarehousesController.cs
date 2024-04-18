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
    public class WarehousesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WarehousesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Warehouses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Warehouses.Include(w => w.Locations);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Warehouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouses = await _context.Warehouses
                .Include(w => w.Locations)
                .FirstOrDefaultAsync(m => m.WAREHOUSE_ID == id);
            if (warehouses == null)
            {
                return NotFound();
            }

            return View(warehouses);
        }

        // GET: Warehouses/Create
        public IActionResult Create()
        {
            ViewData["LOCATION_ID"] = new SelectList(_context.locations, "LOCATION_ID", "ADDRESS");
            return View();
        }

        // POST: Warehouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WAREHOUSE_ID,WAREHOUSE_NAME,LOCATION_ID")] Warehouses warehouses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(warehouses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LOCATION_ID"] = new SelectList(_context.locations, "LOCATION_ID", "ADDRESS", warehouses.LOCATION_ID);
            return View(warehouses);
        }

        // GET: Warehouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouses = await _context.Warehouses.FindAsync(id);
            if (warehouses == null)
            {
                return NotFound();
            }
            ViewData["LOCATION_ID"] = new SelectList(_context.locations, "LOCATION_ID", "ADDRESS", warehouses.LOCATION_ID);
            return View(warehouses);
        }

        // POST: Warehouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WAREHOUSE_ID,WAREHOUSE_NAME,LOCATION_ID")] Warehouses warehouses)
        {
            if (id != warehouses.WAREHOUSE_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(warehouses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehousesExists(warehouses.WAREHOUSE_ID))
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
            ViewData["LOCATION_ID"] = new SelectList(_context.locations, "LOCATION_ID", "ADDRESS", warehouses.LOCATION_ID);
            return View(warehouses);
        }

        // GET: Warehouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouses = await _context.Warehouses
                .Include(w => w.Locations)
                .FirstOrDefaultAsync(m => m.WAREHOUSE_ID == id);
            if (warehouses == null)
            {
                return NotFound();
            }

            return View(warehouses);
        }

        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var warehouses = await _context.Warehouses.FindAsync(id);
            if (warehouses != null)
            {
                _context.Warehouses.Remove(warehouses);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WarehousesExists(int id)
        {
            return _context.Warehouses.Any(e => e.WAREHOUSE_ID == id);
        }
    }
}
