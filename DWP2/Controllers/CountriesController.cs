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
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            var Datos = await _context.countries.Include(c => c.Regions).ToListAsync();
            return View(Datos);
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countries = await _context.countries
                .Include(c => c.Regions)
                .FirstOrDefaultAsync(m => m.COUNTRY_ID == id);
            if (countries == null)
            {
                return NotFound();
            }

            return View(countries);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            ViewData["REGION_ID"] = new SelectList(_context.regions, "REGION_ID", "REGION_NAME");
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("COUNTRY_ID,COUNTRY_NAME,REGION_ID")] Countries countries)
        {
            if (ModelState.IsValid)
            {
                _context.Add(countries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["REGION_ID"] = new SelectList(_context.regions, "REGION_ID", "REGION_NAME", countries.REGION_ID);
            return View(countries);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countries = await _context.countries.FindAsync(id);
            if (countries == null)
            {
                return NotFound();
            }

            // Load regions and populate ViewBag
            ViewBag.RegionList = new SelectList(_context.regions, "REGION_ID", "REGION_NAME", countries.REGION_ID);

            return View(countries);
        }


        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("COUNTRY_ID,COUNTRY_NAME,REGION_ID")] Countries countries)
        {
            if (id != countries.COUNTRY_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(countries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountriesExists(countries.COUNTRY_ID))
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
            ViewData["REGION_ID"] = new SelectList(_context.regions, "REGION_ID", "REGION_ID", countries.REGION_ID);
            return View(countries);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countries = await _context.countries
                .Include(c => c.Regions)
                .FirstOrDefaultAsync(m => m.COUNTRY_ID == id);
            if (countries == null)
            {
                return NotFound();
            }

            return View(countries);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var countries = await _context.countries.FindAsync(id);
            if (countries != null)
            {
                _context.countries.Remove(countries);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountriesExists(string id)
        {
            return _context.countries.Any(e => e.COUNTRY_ID == id);
        }
    }
}
