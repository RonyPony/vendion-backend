using System;
using datingAppBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vendio_backend.Dtos;
using vendio_backend.Models;

namespace vendio_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class vehicleController:ControllerBase
    {
        private readonly vendionContext _context;

        public vehicleController(vendionContext context)
        {
            _context = context;
        }

        // GET: api/vehicle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<vehicle>>> Getvehicles()
        {
            if (_context.Vehicles == null)
            {
                return NotFound();
            }
            return await _context.Vehicles.Where(e => e.isEnabled == true).ToListAsync();
        }

        // GET: api/vehicle/5
        [HttpGet("{id}")]
        public async Task<ActionResult<vehicle>> Getvehicle(int id)
        {
            if (_context.Vehicles == null)
            {
                return NotFound();
            }
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        // PUT: api/vehicle/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putvehicle(int id, vehicle vehicle)
        {
            if (id != vehicle.id)
            {
                return BadRequest();
            }

            _context.Entry(vehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vehicleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/vehicle
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<vehicle>> Postvehicle(vehicle vehicleRegister)
        {
            if (_context.Vehicles == null)
            {
                return Problem("Entity set 'vendionContext.vehicles'  is null.");
            }

            //List<vehicle> vehiclesFound = await _context.Vehicles
            //    .Where(x => x.email == vehicleRegister!.Email)
            //    .ToListAsync();
            //bool emailExists = vehiclesFound.Count <= 0;
            //if (emailExists)
            //{
                
                _context.Vehicles.Add(vehicleRegister);
                await _context.SaveChangesAsync();

                return CreatedAtAction("Getvehicle", new { id = vehicleRegister.id }, vehicleRegister);
            //}
            //else
            //{
            //    return BadRequest("This email is already registered, try to login");
            //}
        }



        // POST: api/enablevehicle/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("enablevehicle/{id}")]
        public async Task<ActionResult<vehicle>> enablevehicle(int id)
        {

            if (_context.Vehicles == null)
            {
                return Problem("Entity set 'vendionContext.vehicles'  is null.");
            }


            if (id != null)
            {
                vehicle vehicle = await _context.Vehicles.FindAsync(id);

                vehicle.isEnabled = true;

                _context.Vehicles.Update(vehicle);
                await _context.SaveChangesAsync();

                return Ok(vehicle);
            }
            else
            {
                return BadRequest("Provide ID");
            }


        }

        // POST: api/desablevehicle/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("disablevehicle/{id}")]
        public async Task<ActionResult<vehicle>> disablevehicle(int id)
        {

            if (_context.Vehicles == null)
            {
                return Problem("Entity set 'vendionContext.vehicles'  is null.");
            }


            if (id != null)
            {
                vehicle vehicle = await _context.Vehicles.FindAsync(id);

                vehicle.isEnabled = false;

                _context.Vehicles.Update(vehicle);
                await _context.SaveChangesAsync();

                return Ok(vehicle);
            }
            else
            {
                return BadRequest("Provide ID");
            }


        }

        // DELETE: api/vehicle/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletevehicle(int id)
        {
            if (_context.Vehicles == null)
            {
                return NotFound();
            }
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool vehicleExists(int id)
        {
            return (_context.Vehicles?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}

