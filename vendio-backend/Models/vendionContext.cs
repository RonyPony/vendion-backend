using System;
using datingAppBackend.Models;
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
    }
}

