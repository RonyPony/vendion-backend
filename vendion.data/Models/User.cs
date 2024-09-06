using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace vendio_backend.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }

        [Required]
        
        public int countryId { get; set; }

        public bool isEnabled { get; set; }

        public bool showNumber { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string phoneNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime bornDate { get; set; }


        public bool deletedAccount { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Name")]
        public string name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Last Name")]
        public string lastName { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Description")]
        public string bio { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Address")]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Fecha de registro")]
        [DataType(DataType.DateTime)]
        public DateTime registerDate { get; set; }

        [DisplayName("Fecha de login")]
        [DataType(DataType.DateTime)]
        public DateTime lastLogin { get; set; }

    }

}