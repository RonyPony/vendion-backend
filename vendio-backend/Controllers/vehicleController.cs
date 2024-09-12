using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vendio_backend.Dtos;
using vendio_backend.Models;
using vendion.core.Interfaces;

namespace vendio_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class vehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public vehicleController(IVehicleService _vehicleservi)
        {
            _vehicleService = _vehicleservi;
        }

        // GET: api/vehicle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> Getvehicles()
        {
            List<Vehicle> all = await _vehicleService.getAllVehiclesAsync();
            if (all == null)
            {
                return NotFound();
            }
            return Ok(all);
        }

        // GET: api/FavVehicles
        [HttpGet]
        [Route("getFavorites/{userId}")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetFavVehicles(int userId)
        {
                return Ok(await _vehicleService.getAllFavoriteVehicles(userId));       
        }


        // GET: api/vehicles/{userId}
        [HttpGet]
        [Route("vehiclesByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> getVehicleByUser(int userId)
        {
            List<Vehicle> vehicle = await _vehicleService.getVehicleByUserIdAsync(userId);

            if (vehicle==null)
            {
                return NotFound("UserNotFound");
            }

            //IEnumerable<Vehicle> vehicles = await _context.Vehicles.Where(e=>e.createdBy==userId).ToListAsync();
            return Ok(vehicle);
        }

        // GET: api/vehicle/offer
        [HttpGet("offer")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetOffer()
        {
            List<Vehicle> off = await _vehicleService.getVehiclesWithOfferAsync();
            if (off == null)
            {
                return NotFound();
            }
            return Ok(off); 
        }

        // GET: api/vehicle/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> Getvehicle(int id)
        {
            Vehicle vehicle =await _vehicleService.getVehicleByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        // PUT: api/vehicle/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putvehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.id)
            {
                return BadRequest();
            }

            _vehicleService.updateVehicleAsync(vehicle);

            return NoContent();
        }

        // POST: api/vehicle
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vehicle>> Postvehicle(newVehicleDto vehicleRegister)
        {

            //List<Vehicle> vehiclesFound = await _context.Vehicles
            //    .Where(x => x.email == vehicleRegister!.Email)
            //    .ToListAsync();
            //bool emailExists = vehiclesFound.Count <= 0;
            //if (emailExists)
            //{
            Vehicle vehicle = new Vehicle();
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

            Vehicle response = await _vehicleService.createVehicle(vehicle);
            

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
        public async Task<ActionResult<Vehicle>> enablevehicle(int id)
        {


            if (id != null)
            {
                Vehicle vehicle = await _vehicleService.getVehicleByIdAsync(id);

                vehicle.isEnabled = true;
                await _vehicleService.updateVehicleAsync(vehicle);

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
        public async Task<ActionResult<Vehicle>> disablevehicle(int id)
        {
            if (id != null)
            {
                Vehicle vehicle = await _vehicleService.getVehicleByIdAsync(id);

                vehicle.isEnabled = false;
                _vehicleService.updateVehicleAsync(vehicle);

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
        public async Task<ActionResult<List<Vehicle>>> addFavorite(int carId,int userId)
        {

            if (carId != null && userId!=null)
            {
                bool isExist = await _vehicleService.isVehicleFavorite(carId, userId);
                
                
                if (isExist)
                {
                    return BadRequest("this vehicle is already in favorites");
                }
                else
                {
                    List<Vehicle> allList = await _vehicleService.addToFavorite(userId, carId);
                    
                    return Ok(allList);
                }

                
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
        public async Task<ActionResult<Vehicle>> removeFavorite(int carId, int userId)
        {

            if (carId != null && userId != null)
            {
                bool isExist = await _vehicleService.isVehicleFavorite(userId,carId);
                List<Vehicle> has = await _vehicleService.getAllFavoriteVehicles(userId);
                if (isExist)
                {
                    foreach (Vehicle item in has)   
                    {
                        _vehicleService.removeFromFavorite(userId, carId);
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
            Vehicle vehicle = await _vehicleService.getVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _vehicleService.deleteVehicleAsync(id);

            return NoContent();
        }

        [HttpGet("makes")]
        public async Task<IActionResult> GetVehicleBrandsAsync()
        {
            List<VehicleBrand> response = _vehicleService.getAllBrandsAsync();
            return Ok(response);
        }
        
        [HttpGet("models/{makeId}")]
        public async Task<IActionResult> GetModels(int makeId)
        {
            List<VehicleModel> response = await _vehicleService.getModelByBrandIdAsync(makeId);
            return Ok(response);
        }


    }
}

