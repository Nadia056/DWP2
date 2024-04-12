
using DWP2.Data;
using DWP2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DWP2.Controllers
{
   
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index( )
        {
            IEnumerable<Customers> listaCustumers = _context.customers;
            return View(listaCustumers);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customers customers)
        {
            if (ModelState.IsValid)
            {
                _context.customers.Add(customers);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var customers = _context.customers.Find(id);
            return View(customers);
        }
        [HttpPost]
        public IActionResult Edit(Customers customers)
        {
            if (ModelState.IsValid)
            {
                _context.customers.Update(customers);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }   

        public IActionResult Details(int id)
        {
            var customers = _context.customers.Find(id);
            return View(customers);
        }   
        public IActionResult Delete(int id)
        {
            var customers = _context.customers.Find(id);
            _context.customers.Remove(customers);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
