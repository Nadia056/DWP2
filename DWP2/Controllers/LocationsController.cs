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
    public class LocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
            var locations = await _context.locations.Include(l => l.Countries).ToListAsync();
            return View(locations);
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.locations
                .Include(l => l.Countries)
                .FirstOrDefaultAsync(m => m.LOCATION_ID == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            ViewData["COUNTRY_ID"] = new SelectList(_context.countries, "COUNTRY_ID", "COUNTRY_NAME");
            return View();
        }

        // POST: Locations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LOCATION_ID,ADDRESS,POSTAL_CODE,CITY,STATE,COUNTRY_ID")] Locations location)
        {
            if (ModelState.IsValid)
            {
                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["COUNTRY_ID"] = new SelectList(_context.countries, "COUNTRY_ID", "COUNTRY_NAME", location.COUNTRY_ID);
            return View(location);
        }

        // GET: Locations/Edit/5
        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            // Cargar los países y pasarlos a ViewBag
            ViewData["COUNTRY_ID"] = new SelectList(_context.countries, "COUNTRY_ID", "COUNTRY_NAME", location.COUNTRY_ID);

            return View(location);
        }


        // POST: Locations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LOCATION_ID,ADDRESS,POSTAL_CODE,CITY,STATE,COUNTRY_ID")] Locations location)
        {
            if (id != location.LOCATION_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.LOCATION_ID))
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
            ViewData["COUNTRY_ID"] = new SelectList(_context.countries, "COUNTRY_ID", "COUNTRY_NAME", location.COUNTRY_ID);
            return View(location);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.locations
                .Include(l => l.Countries)
                .FirstOrDefaultAsync(m => m.LOCATION_ID == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _context.locations.FindAsync(id);
            if (location != null)
            {
                _context.locations.Remove(location);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _context.locations.Any(e => e.LOCATION_ID == id);
        }
    }
}
