using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using vendio_backend.Models;

namespace vendio_backend.Dtos
{
    public class newVehicleDto
    {
        
        public int createdBy { get; set; }

        //public int features { get; set; }

        public long price { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string contactPhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Name")]
        public string name { get; set; }


        [DataType(DataType.Text)]
        [DisplayName("Description")]
        public string description { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Brand")]
        public string brand { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Model")]
        public string model { get; set; }

        [Required]
        [DisplayName("Year")]
        public string year { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("vim no.")]
        public string vim { get; set; }

        [Required]
        [DisplayName("Condition")]
        public string condition { get; set; }

        [Required]
        [DisplayName("Location")]
        public string location { get; set; }
    }
}

