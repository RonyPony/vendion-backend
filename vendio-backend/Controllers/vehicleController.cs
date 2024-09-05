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
    public class vehicleController : ControllerBase
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

        // GET: api/FavVehicles
        [HttpGet]
        [Route("getFavorites/{userId}")]
        public async Task<ActionResult<IEnumerable<favoriteVehiclesMapping>>> GetFavVehicles(int userId)
        {
            if (_context.Vehicles == null)
            {
                return NotFound();
            }
            return await _context.favoritesMapping.Where(e => e.userId == userId).ToListAsync();
        }


        // GET: api/vehicles/{userId}
        [HttpGet]
        [Route("vehiclesByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<vehicle>>> getVehicleByUser(int userId)
        {
            User x =await _context.Users.FindAsync(userId);


            if (x == null)
            {
                return NotFound("UserNotFound");
            }

            //IEnumerable<vehicle> vehicles = await _context.Vehicles.Where(e=>e.createdBy==userId).ToListAsync();
            return await _context.Vehicles.Where(e => e.createdBy == userId).ToListAsync();
        }

        // GET: api/vehicle/offer
        [HttpGet("offer")]
        public async Task<ActionResult<IEnumerable<vehicle>>> GetOffer()
        {
            if (_context.Vehicles == null)
            {
                return NotFound();
            }
            return await _context.Vehicles.Where(e => e.isEnabled == true && e.isOffer).ToListAsync();
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
        public async Task<ActionResult<vehicle>> Postvehicle(newVehicleDto vehicleRegister)
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
            vehicle vehicle = new vehicle();
            vehicle.brand = vehicleRegister.brand;
            vehicle.condition = vehicleRegister.condition;
            vehicle.contactPhoneNumber = vehicleRegister.contactPhoneNumber;
            vehicle.createdBy = vehicleRegister.createdBy;
            vehicle.description = vehicleRegister.description;
            vehicle.features = vehicleRegister.features;
            vehicle.model = vehicleRegister.model;
            vehicle.name = vehicleRegister.name;
            vehicle.price = vehicleRegister.price;
            vehicle.vim = vehicleRegister.vim;
            vehicle.year = vehicleRegister.year;
            vehicle.isEnabled = true;
            vehicle.location = vehicleRegister.location;
            vehicle.isOffer = false;
            vehicle.isPublished = true;
            vehicle.modificationDate = DateTime.Now;
            
            vehicle.registerDate = DateTime.Now;

            if (vehicle.brand.Length <= 2)
            {
                return BadRequest("Not valid brand");
            }
            if (vehicle.createdBy<=0 )
            {
                return BadRequest("Not valid creator");
            }
            if (vehicle.model.Length <= 2)
            {
                return BadRequest("Not valid model");
            }
            if (vehicle.name.Length <= 2 || vehicle.name.Length>30)
            {
                return BadRequest("Not valid title");
            }
            if (vehicle.year.Length <= 1)
            {
                return BadRequest("Not valid year");
            }

            _context.Vehicles.Add(vehicle);
                await _context.SaveChangesAsync();

                return CreatedAtAction("Getvehicle", new { id = vehicle.id }, vehicle);
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


        // POST: api/addFavorite/1/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("addFavorite/{carId}/{userId}")]
        public async Task<ActionResult<vehicle>> addFavorite(int carId,int userId)
        {

            if (_context.Vehicles == null)
            {
                return Problem("Entity set 'vendionContext.vehicles'  is null.");
            }


            if (carId != null && userId!=null)
            {
                List<favoriteVehiclesMapping> has = await _context.favoritesMapping.Where(e=>e.userId ==userId && e.vehicleId ==carId).ToListAsync();
                if (has.Count>=1)
                {
                    return Ok("this vehicle is already in favorites");
                }
                else
                {
                    vehicle vehicle = await _context.Vehicles.FindAsync(carId);
                    User user = await _context.Users.FindAsync(userId);
                    favoriteVehiclesMapping fav = new favoriteVehiclesMapping();
                    fav.userId = user.id;
                    fav.vehicleId = vehicle.id;
                    fav.CreatedAt = DateTime.UtcNow;
                    _context.favoritesMapping.AddAsync(fav);
                    await _context.SaveChangesAsync();
                    return Ok(fav);
                }

                return NotFound();
            }
            else
            {
                return BadRequest("Provide the correct info");
            }


        }


        // POST: api/removeFavorite/1/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("removeFavorite/{carId}/{userId}")]
        public async Task<ActionResult<vehicle>> removeFavorite(int carId, int userId)
        {

            if (_context.Vehicles == null)
            {
                return Problem("Entity set 'vendionContext.vehicles'  is null.");
            }


            if (carId != null && userId != null)
            {
                List<favoriteVehiclesMapping> has = await _context.favoritesMapping.Where(e => e.userId == userId && e.vehicleId == carId).ToListAsync();
                if (has.Count >= 1)
                {
                    foreach (var item in has)   
                    {
                        _context.favoritesMapping.Remove(item);
                        _context.SaveChanges();
                    }
                    
                    return Ok("Removed Successfully");
                }
                else
                {
                    return Ok("this vehicle is not in favorites");
                    
                }

                return NotFound();
            }
            else
            {
                return BadRequest("Provide the correct info");
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

