using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace vendio_backend.Models
{
    public class vehicleFeature
    {
        [Key]
        public int id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Name")]
        public string name { get; set; }
        [Required]
        [DisplayName("Active")]
        public bool enabled { get; set; }
    }
}

