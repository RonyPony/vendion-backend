using System;
using System.ComponentModel.DataAnnotations;

namespace vendio_backend.Models
{
    public class favoriteVehiclesMapping
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Text)]
        public int vehicleId { get; set; }
        public int userId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
    }
}

