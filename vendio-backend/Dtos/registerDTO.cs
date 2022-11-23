using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace vendio_backend.Dtos
{
    public class registerDTO
    {
        [Required]
        [DisplayName("Name")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required]
        [DisplayName("Lastname")]
        [DataType(DataType.Text)]
        public string lastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone Number")]
        public string phone { get; set; }
    }
}

