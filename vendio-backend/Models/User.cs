using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace datingAppBackend.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int countryId { get; set; }

        public bool isEnabled { get; set; }

        public bool showNumber { get; set; }

        public string phoneNumber { get; set; }

        public DateTime bornDate { get; set; }


        public bool deletedAccount { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("User Name")]
        public string name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Last Name")]
        public string lastName { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Description")]
        public string bio { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Email Address")]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Fecha de registro")]
        public DateTime registerDate { get; set; }

        [DisplayName("Fecha de login")]
        public DateTime lastLogin { get; set; }

    }

}