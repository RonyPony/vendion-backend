
using datingAppBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using vendio_backend.Models;

namespace vendio_backend.Controllers
{
    [Route("api/photos")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        //private readonly IPhotoService _photoService;
        private readonly vendionContext _context;

        public PhotoController(vendionContext ctx)
        {
            _context = ctx;
            //_photoService = photoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPhotos()
        {
            try
            {
                //IEnumerable<Photo> photos = await _photoService.GetPhotos();
                IEnumerable<Photo> photos = await _context.photos.ToListAsync();
                return Ok(photos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            try
            {

                //Photo photo = await _photoService.GetPhotoById(id);
                Photo photo = await _context.photos.FirstOrDefaultAsync(photo => photo.Id == id);

                return Ok(photo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("makeProductPicture/{photoId}")]
        public async Task<IActionResult> SetAsPresentationPicture(int photoId)
        {
            try
            {
                Photo foto = await _context.photos.FindAsync(photoId);
                IEnumerable<Photo> listaPhotos = await _context.photos.Where(e => e.Id == photoId && e.isProductPicture == true).ToListAsync();
                foreach (Photo ft in listaPhotos)
                {
                    ft.isProductPicture = false;
                    _context.photos.Update(ft);
                    _context.SaveChangesAsync();
                }
                if (foto==null)
                {
                    return NotFound();
                }
                foto.isProductPicture = true;
                _context.SaveChanges();
                return Ok(foto);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpGet("gallery/{vehicleId}")]
        public async Task<IActionResult> GetVehiclePhoto(int vehicleId)
        {
            try
            {
                //IEnumerable<Photo> photos = await _photoService.GetPhotosByUserId(userId);
                IEnumerable<Photo> photos = await _context.photos.Where(r=>r.productId == vehicleId).ToListAsync();
                return Ok(photos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("byVehicle/{vehicleId}")]
        public async Task<IActionResult> GetVehiclePicture(int vehicleId)
        {
            if (vehicleId==null)
            {
                return BadRequest("Invalid vehicleId");
            }
            try
            {
                //IEnumerable<Photo> photos = await _photoService.GetPhotosByUserId(userId);
                Photo? photo = await _context.photos.OrderByDescending(r => r.CreatedAt).Where(r => r.productId == vehicleId && r.isProductPicture).FirstOrDefaultAsync();
                if (photo ==null)
                {
                    return NotFound("No product picture found");
                }
                return Ok(photo);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("new")]
        public async Task<IActionResult> PostPhoto([FromForm] PhotoToRegister photoToRegister)
        {
            try
            {
                string[] extensions = new string[] { ".jpg", ".png", ".svg", "jpeg" };
                var fileName = Path.GetFileName(photoToRegister.Image.FileName);
                var fileExtension = Path.GetExtension(fileName).ToLower();

                if (!extensions.Contains(fileExtension))
                    throw new ArgumentException("{0} is an InvalidFileExtention", fileExtension);

                vehicle foundVehicle = await _context.Vehicles.FindAsync(photoToRegister.productId);

                if (foundVehicle is null)
                    NotFound("VehicleNotFound");

                var newPhoto = new Photo
                {
                    Name = fileName.Split('.')[0],
                    CreatedAt = DateTime.UtcNow,
                    isProductPicture = false,
                    productId = photoToRegister.productId,
                    
                };

                using (var target = new MemoryStream())
                {
                    

                    photoToRegister.Image.CopyTo(target);
                    newPhoto.Image = target.ToArray();
                }

                //await _photoService.CreatePhoto(newPhoto);
                _context.Add(newPhoto);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetPhoto", new { id = newPhoto.Id }, newPhoto);
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            try
            {
                //Photo photo = await _photoService.GetPhotoById(id);
                Photo photo = await _context.photos.FindAsync(id);
                _context.Remove(photo);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
