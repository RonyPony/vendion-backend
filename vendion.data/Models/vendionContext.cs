using System;
using vendio_backend.Models;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;

namespace vendio_backend.Models
{
    public class vendionContext: DbContext
    {
        public vendionContext(DbContextOptions<vendionContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; } = null;
        public DbSet<Vehicle> Vehicles { get; set; } = null;
        public DbSet<VehicleBrand> VehicleBrands { get; set; } = null;
        public DbSet<VehicleModel> VehicleModels { get; set; } = null;
        public DbSet<Photo> photos { get; set; } = null;
        public DbSet<favoriteVehiclesMapping> favoritesMapping { get; set; } = null;
    }
}

