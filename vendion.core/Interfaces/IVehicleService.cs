
using vendio_backend.Models;

namespace vendion.core.Interfaces
{
    public interface IVehicleService
    {
        public Task<List<Vehicle>> getAllVehiclesAsync();
        public Task<Vehicle> createVehicle(Vehicle data);
        public Task<Vehicle> getVehicleByIdAsync(int id);
        public Task<List<Vehicle>> getVehicleByUserIdAsync(int id);
        public Task<Vehicle> updateVehicleAsync(Vehicle data);
        public Task<Vehicle> deleteVehicleAsync(int Id);

        public Task<List<Vehicle>> getAllFavoriteVehicles(int userId);
        public Task<List<Vehicle>> addToFavorite(int userId,int vehicleId);
        public Task<List<Vehicle>> removeFromFavorite(int userId, int vehicleId);
        public Task<bool> isVehicleFavorite(int userId, int carId);

        public List<VehicleBrand> getAllBrandsAsync();
        public VehicleBrand getBrandById(int id);
        public VehicleBrand updateBrand(VehicleBrand data);
        public VehicleBrand deleteBrand(int brandId);

        public VehicleModel[] getAllBrandModels(int brandId);
        public VehicleModel getModelById(int modelId);
        public VehicleModel updateModel(VehicleModel data);
        public VehicleModel deleteModel(int modelId);

        public Task<List<Vehicle>> getVehiclesWithOfferAsync();
    }
}
