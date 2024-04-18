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
    public class Order_ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Order_ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Order_Items
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Order_Items.Include(o => o.Products);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Order_Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order_Items = await _context.Order_Items
                .Include(o => o.Products)
                .FirstOrDefaultAsync(m => m.ORDER_ID == id);
            if (order_Items == null)
            {
                return NotFound();
            }

            return View(order_Items);
        }

        // GET: Order_Items/Create
        public IActionResult Create()
        {
            ViewData["PRODUCT_ID"] = new SelectList(_context.Products, "PRODUCT_ID", "PRODUCT_NAME");
            return View();
        }

        // POST: Order_Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ORDER_ID,ITEM_ID,PRODUCT_ID,QUANTITY,UNIT_PRICE")] Order_Items order_Items)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order_Items);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PRODUCT_ID"] = new SelectList(_context.Products, "PRODUCT_ID", "PRODUCT_NAME", order_Items.PRODUCT_ID);
            return View(order_Items);
        }

        // GET: Order_Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order_Items = await _context.Order_Items.FindAsync(id);
            if (order_Items == null)
            {
                return NotFound();
            }
            ViewData["PRODUCT_ID"] = new SelectList(_context.Products, "PRODUCT_ID", "PRODUCT_NAME", order_Items.PRODUCT_ID);
            return View(order_Items);
        }

        // POST: Order_Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ORDER_ID,ITEM_ID,PRODUCT_ID,QUANTITY,UNIT_PRICE")] Order_Items order_Items)
        {
            if (id != order_Items.ORDER_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order_Items);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Order_ItemsExists(order_Items.ORDER_ID))
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
            ViewData["PRODUCT_ID"] = new SelectList(_context.Products, "PRODUCT_ID", "PRODUCT_NAME", order_Items.PRODUCT_ID);
            return View(order_Items);
        }

        // GET: Order_Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order_Items = await _context.Order_Items
                .Include(o => o.Products)
                .FirstOrDefaultAsync(m => m.ORDER_ID == id);
            if (order_Items == null)
            {
                return NotFound();
            }

            return View(order_Items);
        }

        // POST: Order_Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var order_Items = await _context.Order_Items.FindAsync(id);
            if (order_Items != null)
            {
                _context.Order_Items.Remove(order_Items);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Order_ItemsExists(int? id)
        {
            return _context.Order_Items.Any(e => e.ORDER_ID == id);
        }
    }
}
