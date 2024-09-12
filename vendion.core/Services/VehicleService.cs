using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vendio_backend.Models;
using vendion.core.Interfaces;

namespace vendion.core.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly vendionContext _context;

        public VehicleService(vendionContext context)
        {
            this._context = context;
        }

        public VehicleBrand deleteBrand(int brandId)
        {
            throw new NotImplementedException();
        }

        public VehicleModel deleteModel(int modelId)
        {
            throw new NotImplementedException();
        }

        public async Task<Vehicle> deleteVehicleAsync(int Id)
        {
            try
            {
                Vehicle vv = await getVehicleByIdAsync(Id);
                _context.Vehicles.Remove(vv);
                await _context.SaveChangesAsync();
                return vv;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public VehicleModel[] getAllBrandModels(int brandId)
        {
            throw new NotImplementedException();
        }

        public List<VehicleBrand> getAllBrandsAsync()
        {
            try
            {
                return _context.VehicleBrands.Where(ee => ee.isEnabled).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Vehicle>> getAllVehiclesAsync()
        {
            try
            {
                return await _context.Vehicles.Where(e => e.isEnabled == true).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<List<VehicleModel>> getModelByBrandIdAsync(int brandId)
        {
            VehicleBrand brand =await getBrandByIdAsync(brandId);
            List<VehicleModel> model = await _context.VehicleModels
                .Where(ee=>ee.brandId == brandId)
                .Where(ee=>ee.isEnabled)
                .ToListAsync();
            return model;
        }

        public async Task<VehicleBrand> getBrandByIdAsync(int id)
        {
            return await _context.VehicleBrands.FindAsync(id);
        }

        public VehicleModel getModelById(int modelId)
        {
            throw new NotImplementedException();
        }

        public async Task<Vehicle> getVehicleByIdAsync(int id)
        {
            try
            {
                return await _context.Vehicles.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public VehicleBrand updateBrand(VehicleBrand data)
        {
            throw new NotImplementedException();
        }

        public VehicleModel updateModel(VehicleModel data)
        {
            throw new NotImplementedException();
        }

        public async Task<Vehicle> updateVehicleAsync(Vehicle data)
        {
            _context.Entry(data).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return data;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vehicleExists(data.id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        private bool vehicleExists(int id)
        {
            return (_context.Vehicles?.Any(e => e.id == id)).GetValueOrDefault();
        }

        public async Task<List<Vehicle>> getAllFavoriteVehicles(int userId)
        {
            try
            {
                List<Vehicle> finalList = new List<Vehicle>();
                List<favoriteVehiclesMapping> favList = await _context.favoritesMapping.Where(e => e.userId == userId).ToListAsync();
                foreach (favoriteVehiclesMapping item in favList)
                {
                    Vehicle car = await getVehicleByIdAsync(item.vehicleId);
                    if (car != null)
                    {
                        finalList.Add(car);
                    }
                }
                return finalList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<Vehicle>> getVehicleByUserIdAsync(int id)
        {
            try
            {
                return await _context.Vehicles
                    .Where(e => e.createdBy == id)
                    .Where(e=> e.isEnabled == true)
                    .ToListAsync();
               
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<List<Vehicle>> getVehiclesWithOfferAsync()
        {
            try
            {
               return await _context.Vehicles.Where(e => e.isEnabled == true && e.isOffer).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Vehicle> createVehicle(Vehicle data)
        {
            try
            {
                _context.Vehicles.Add(data);
                await _context.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<bool> isVehicleFavorite(int userId, int carId)
        {
            try
            {
                List<favoriteVehiclesMapping> favMap = await _context.favoritesMapping.Where(e => e.userId == userId && e.vehicleId == carId).ToListAsync();
                return favMap.Any();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<List<Vehicle>> addToFavorite(int userId, int vehicleId)
        {
            try
            {
                Vehicle vehicle = await getVehicleByIdAsync(vehicleId);
                User user = await _context.Users.FindAsync(userId);
                favoriteVehiclesMapping fav = new favoriteVehiclesMapping();
                fav.userId = user.id;
                fav.vehicleId = vehicle.id;
                fav.CreatedAt = DateTime.UtcNow;
                _context.favoritesMapping.AddAsync(fav);
                await _context.SaveChangesAsync();
                List<Vehicle> allFav =await  getAllFavoriteVehicles(userId);
                return allFav;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<Vehicle>> removeFromFavorite(int userId, int vehicleId)
        {
            try
            {
                Vehicle vehicle = await getVehicleByIdAsync(vehicleId);
                User user = await _context.Users.FindAsync(userId);

                favoriteVehiclesMapping ffmp=  _context.favoritesMapping
                    .Where(ee => ee.vehicleId == vehicleId)
                    .Where(aa => aa.userId == userId).FirstOrDefault();
                _context.Remove(ffmp);
                await _context.SaveChangesAsync();
                List<Vehicle> allFav = await getAllFavoriteVehicles(userId);
                return allFav;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        
    }
}
