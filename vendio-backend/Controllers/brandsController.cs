using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using datingAppBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vendio_backend.Models;

namespace vendio_backend.Controllers
{
    public class brandsController : Controller

        
    
    {
       private readonly vendionContext _context;
        public brandsController( vendionContext context)
        {
            _context = context;
        }
     
        [Route("api/brands")]

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarBrand>>> getBrandsAsync()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.brands.Where(e => e.isActive == true).ToListAsync();

        }


        // POST: brands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // POST: brands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: brands/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}