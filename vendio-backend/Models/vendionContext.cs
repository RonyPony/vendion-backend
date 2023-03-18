using System;
using datingAppBackend.Models;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;
using vendio_backend.Controllers;

namespace vendio_backend.Models
{
    public class vendionContext : DbContext
    {
        public vendionContext(DbContextOptions<vendionContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; } = null;
        public DbSet<vehicle> Vehicles { get; set; }
        //public DbSet<vehicleFeature> vehicleFeatures { get; set; }
        public DbSet<Photo> photos { get; set; } = null;
        public DbSet<CarBrand>brands{get;set;}
        public DbSet<favoriteVehiclesMapping> favoritesMapping { get; set; } = null;
    }
}

