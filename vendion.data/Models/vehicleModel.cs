using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace vendio_backend.Models
{
    public class VehicleModel
    {
        [Key]
        public int id { get; set; }
        public int brandId { get; set; }

        public bool isEnabled { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Name")]
        public string name { get; set; }



    }
}

