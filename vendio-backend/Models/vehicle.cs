using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace vendio_backend.Models
{
    public class vehicle
    {
        [Key]
        public int id { get; set; }
        public int createdBy { get; set; }

        public bool isEnabled { get; set; }
        public bool isPublished { get; set; }
        [DefaultValue(false)]
        public bool isOffer { get; set; }

        public List<String> features { get; set; }

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

        [DisplayName("Fecha de creacion")]
        public DateTime registerDate { get; set; }

        [DisplayName("Fecha de modificacion")]
        [DataType(DataType.DateTime)]
        public DateTime modificationDate { get; set; }

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

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("vim no.")]
        public string vim { get; set; }

        [Required]
        [DisplayName("Condition")]
        public string condition { get; set; }


    }
}

