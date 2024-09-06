using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace vendio_backend.Models
{
    public class VehicleBrand
    {
        [Key]
        public int id { get; set; }

        public bool isEnabled { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Name")]
        public string name { get; set; }


        [DataType(DataType.Text)]
        [DisplayName("logoUrl")]
        public string logoUrl { get; set; }


    }
}

