using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vendio_backend.Dtos;
using vendio_backend.Models;

namespace vendio_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly vendionContext _context;

        public userController(vendionContext context)
        {
            _context = context;
        }

        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.Where(e => e.isEnabled == true).ToListAsync();
        }

        // GET: api/user/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/user/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/user
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(registerDTO userRegister)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'vendionContext.Users'  is null.");
            }

            List<User> usersFound = await _context.Users
                .Where(x => x.email == userRegister!.Email)
                .ToListAsync();
            bool emailExists = usersFound.Count <= 0;
            if (emailExists)
            {
                User user = new User();
                user.name = userRegister.name;
                user.email = userRegister.Email;
                user.Password = userRegister.Password;
                user.lastName = userRegister.lastName;
                user.isEnabled = true;
                user.bio = "N/A";
                user.registerDate = DateTime.UtcNow;
                user.lastLogin = DateTime.UtcNow;
                user.deletedAccount = false;
                
                user.showNumber = true;
                user.phoneNumber = userRegister.phone;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { id = user.id }, user);
            }
            else
            {
                return BadRequest("duplicateEmail");
            }
        }

        [HttpPost]
        [Route("findByEmail/{email}")]
        public async Task<ActionResult<User>> findUserByEmail(String email)
        {
            if (email == String.Empty)
            {
                return BadRequest("emptyEmail");
            }

            try
            {
                var user = (from x in _context.Users
                            where x.email == email
                            select x).FirstOrDefault();
                if (user == null)
                {
                    return NotFound("userNotFound");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("addVehicleToFav/{userId}/{vehicleId}")]
        public async Task<ActionResult<User>> addVehicleToFav(int userId,int vehicleId)
        {
            if (userId == null || userId==0 || vehicleId == null || vehicleId == 0)
            {
                return BadRequest("infoMissing");
            }

            try
            {
                var user = (from x in _context.Users
                            where x.id == userId &&
                            x.isEnabled
                            select x).FirstOrDefault();
                if (user == null)
                {
                    return NotFound("userNotFound");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/login
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> loginUser(loginDTO userLogin)
        {

            if (_context.Users == null)
            {
                return Problem("Entity set 'vendionContext.Users'  is null.");
            }
            var users = (from x in _context.Users
                         where x.email == userLogin.email
                         where x.Password == userLogin.Password

                         select x).FirstOrDefault();

            int userId = 0;

            

            if (users != null)
            {
                if (!users.isEnabled)
                {
                    return StatusCode(StatusCodes.Status423Locked, "Current User (" + users.email + ") is locked by the admin, we will review your account just to make sure, keep an eye on your email inbox, or contact us");
                }

                userId = users.id;
            }
            else
            {
                return Unauthorized("credentialsError");
            }

            User user = await _context.Users.FindAsync(userId);

            user.lastLogin = DateTime.UtcNow;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }



        // POST: api/enableUser/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("enableUser/{id}")]
        public async Task<ActionResult<User>> enableUser(int id)
        {

            if (_context.Users == null)
            {
                return Problem("Entity set 'vendionContext.Users'  is null.");
            }


            if (id != null)
            {
                User user = await _context.Users.FindAsync(id);

                user.isEnabled = true;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return Ok(user);
            }
            else
            {
                return BadRequest("provideID");
            }


        }

        // POST: api/desableUser/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("disableUser/{id}")]
        public async Task<ActionResult<User>> disableUser(int id)
        {

            if (_context.Users == null)
            {
                return Problem("Entity set 'vendionContext.Users'  is null.");
            }


            if (id != null)
            {
                User user = await _context.Users.FindAsync(id);

                user.isEnabled = false;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return Ok(user);
            }
            else
            {
                return BadRequest("ProvideID");
            }


        }

        // DELETE: api/user/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}

