using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace vendio_backend.Dtos
{
    public class loginDTO
    {
            [Required]
            [DisplayName("User email")]
            public string UserEmail { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [DisplayName("Password")]
            public string Password { get; set; }

            [DisplayName("Remember me?")]
            public bool RememberMe { get; set; }
        
    
    }
}

